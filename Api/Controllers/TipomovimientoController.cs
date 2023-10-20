using System;
using System.Collections.Generic;
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

namespace Api.Controllers
{
[ApiVersion("1.0")]
[ApiVersion("1.1")]

    public class TipomovimientoController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TipomovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<TipoMovimientoDto>>> Get()
        {
            var tipoMovimientos = await unitOfWork.TipoMovimientos.GetAllAsync();
            return mapper.Map<List<TipoMovimientoDto>>(tipoMovimientos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TipoMovimientoDto>> GetByIdAsync(int id)
        {
            var tipomovimiento = await unitOfWork.TipoMovimientos.GetByIdAsync(id);
            if (tipomovimiento == null)
            {
                return NotFound();
            }
            return mapper.Map<TipoMovimientoDto>(tipomovimiento);
        }  
        [HttpGet("1.1")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TipoMovimientoDto>>> GetPagination([FromQuery] Params tipoMovimientoParams)
        {
            var entidad = await unitOfWork.TipoMovimientos.GetAllAsync(tipoMovimientoParams.PageIndex, tipoMovimientoParams.PageSize, tipoMovimientoParams.Search);
            var listEntidad = mapper.Map<List<TipoMovimientoDto>>(entidad.registros);
            return new Pager<TipoMovimientoDto>(listEntidad, entidad.totalRegistros, tipoMovimientoParams.PageIndex, tipoMovimientoParams.PageSize, tipoMovimientoParams.Search);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<TipoMovimiento>> Post(TipoMovimientoDto tipoMovimientoDto)
        {
            var tipoMovimiento = mapper.Map<TipoMovimiento>(tipoMovimientoDto);
            unitOfWork.TipoMovimientos.Add(tipoMovimiento);
            await unitOfWork.SaveAsync();
            if (tipoMovimiento== null)
            {
                return BadRequest();
            }

            tipoMovimiento.Id = tipoMovimiento.Id;
            return CreatedAtAction(nameof(Post), new { id = tipoMovimiento.Id }, tipoMovimientoDto);
        }  

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<TipoMovimientoDto>> Put(int id, [FromBody]TipoMovimientoDto tipoMovimientoDto)
        {
            if (tipoMovimientoDto == null)
            {
                return BadRequest();
            }
            var tipoMovimiento = mapper.Map<TipoMovimiento>(tipoMovimientoDto);
            unitOfWork.TipoMovimientos.Update(tipoMovimiento);
            await unitOfWork.SaveAsync();
            return tipoMovimientoDto;
        } 

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var tipoMovimiento = await unitOfWork.TipoMovimientos.GetByIdAsync(id);
            if (tipoMovimiento == null)
            {
                return NotFound();
            }

            unitOfWork.TipoMovimientos.Remove(tipoMovimiento);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}