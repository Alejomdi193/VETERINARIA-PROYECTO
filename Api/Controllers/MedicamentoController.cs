using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
using Aplicacion.UnitOfWork;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize]
    public class MedicamentoController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MedicamentoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
        {
            var medicamento = await unitOfWork.Medicamentos.GetAllAsync();
            return mapper.Map<List<MedicamentoDto>>(medicamento);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MedicamentoDto>> GetByIdAsync(int id)
        {
            var medicamento = await unitOfWork.Medicamentos.GetByIdAsync(id);
            if( medicamento == null)
            {
                return NotFound();
            }
            return mapper.Map<MedicamentoDto>(medicamento);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicamentoDto>>> GetPagination([FromQuery] Params paisParams)
        {
            var entidad = await unitOfWork.Medicamentos.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
            var listEntidad = mapper.Map<List<MedicamentoDto>>(entidad.registros);
            return new Pager<MedicamentoDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        }

        [HttpGet("Genfar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get2()
        {
            var medicamento = await unitOfWork.Medicamentos.MedicamentoGenfar();
            if( medicamento == null)
            {
                return NotFound();
            }
            return mapper.Map<List<MedicamentoDto>>(medicamento);
        }

        [HttpGet("50000")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get5()
        {
            var medicamento = await unitOfWork.Medicamentos.VentaMayor();
            if( medicamento == null)
            {
                return NotFound();
            }
            return mapper.Map<List<MedicamentoDto>>(medicamento);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Medicamento>> Post(MedicamentoDto medicamentoDto)
        {
            var medicamento = mapper.Map<Medicamento>(medicamentoDto);
            unitOfWork.Medicamentos.Add(medicamento);
            await unitOfWork.SaveAsync();
            if(medicamento == null)
            {
                return BadRequest();
            }

            medicamento.Id = medicamento.Id;
            return CreatedAtAction(nameof(Post), new{id = medicamentoDto.Id},medicamentoDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MedicamentoDto>> Put (int id, [FromBody] MedicamentoDto medicamentoDto)
        {
                if(medicamentoDto == null)
                {
                    return BadRequest();
                }
                var medicamento = mapper.Map<Medicamento>(medicamentoDto);
                unitOfWork.Medicamentos.Update(medicamento);
                await unitOfWork.SaveAsync();
                return medicamentoDto;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var medicamento = await unitOfWork.Medicamentos.GetByIdAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            unitOfWork.Medicamentos.Remove(medicamento);
            await unitOfWork.SaveAsync();
            return NoContent();
        }

        



    }
}