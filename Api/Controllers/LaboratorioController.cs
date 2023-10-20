using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Api.Helpers.Errors;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace Api.Controllers
{
[ApiVersion("1.0")]
[ApiVersion("1.1")]

    public class LaboratorioController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LaboratorioController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<LaboratorioDto>>> Get()
        {
            var laboratorio = await unitOfWork.Laboratorios.GetAllAsync();
            if(laboratorio == null)
            {
                return BadRequest();
            }
            return mapper.Map<List<LaboratorioDto>>(laboratorio);


        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<LaboratorioDto>> Get(int id)
        {
            var laboratorio = await unitOfWork.Laboratorios.GetByIdAsync(id);
            if(laboratorio == null)
            {
                return NotFound();
            }
            return mapper.Map<LaboratorioDto>(laboratorio);
        }
        [HttpGet("1.1")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<LaboratorioDto>>> GetPagination([FromQuery] Params laboratorioParams)
        {
            var entidad = await unitOfWork.Laboratorios.GetAllAsync(laboratorioParams.PageIndex, laboratorioParams.PageSize, laboratorioParams.Search);
            var listEntidad = mapper.Map<List<LaboratorioDto>>(entidad.registros);
            return new Pager<LaboratorioDto>(listEntidad, entidad.totalRegistros, laboratorioParams.PageIndex, laboratorioParams.PageSize, laboratorioParams.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<Laboratorio>>> Post(LaboratorioDto laboratorioDto)
        {
            var laboratorio = mapper.Map<Laboratorio>(laboratorioDto);
            unitOfWork.Laboratorios.Add(laboratorio);
            await unitOfWork.SaveAsync();
            if(laboratorio == null)
            {
                return BadRequest();
            }
            laboratorio.Id = laboratorio.Id;
            return CreatedAtAction(nameof(Post), new{id = laboratorioDto.Id},laboratorioDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<LaboratorioDto>> Put(int id ,[FromBody] LaboratorioDto laboratorioDto)
        {
            if(laboratorioDto == null)
            {
                return BadRequest();
            }

            var laboratorio = mapper.Map<Laboratorio>(laboratorioDto);
            unitOfWork.Laboratorios.Add(laboratorio);
            await unitOfWork.SaveAsync();
            return laboratorioDto;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Delete(int id)
        {
            var laboratio = await unitOfWork.Laboratorios.GetByIdAsync(id);
            if(laboratio == null)
            {
                return NotFound();
            }
            unitOfWork.Laboratorios.Remove(laboratio);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}