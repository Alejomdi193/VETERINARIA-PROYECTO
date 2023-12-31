using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Api.Helpers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
[ApiVersion("1.0")]
[ApiVersion("1.1")]

    public class ProveedorController : BaseApiController

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
        {
            var proveedor = await unitOfWork.Proveedores.GetAllAsync();
            return mapper.Map<List<ProveedorDto>>(proveedor);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ProveedorDto>> GetByIdAsync(int id)
        {
            var proveedor = await unitOfWork.Proveedores.GetByIdAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return mapper.Map<ProveedorDto>(proveedor);
        }  

        [HttpGet("proveedorxMedicamento/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get10(string nombre)
        {
            var proveedor = await unitOfWork.Proveedores.ProveedoresxMedicamento(nombre);
            if (proveedor == null)
            {
                return NotFound();
            }
            return mapper.Map<List<ProveedorDto>>(proveedor);
        }  
        [HttpGet("1.1")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProveedorDto>>> GetPagination([FromQuery] Params proveedorParams)
        {
            var entidad = await unitOfWork.Proveedores.GetAllAsync(proveedorParams.PageIndex, proveedorParams.PageSize, proveedorParams.Search);
            var listEntidad = mapper.Map<List<ProveedorDto>>(entidad.registros);
            return new Pager<ProveedorDto>(listEntidad, entidad.totalRegistros, proveedorParams.PageIndex, proveedorParams.PageSize, proveedorParams.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Proveedor>> Post(ProveedorDto proveedorDto)
        {
            var proveedor = mapper.Map<Proveedor>(proveedorDto);
            unitOfWork.Proveedores.Add(proveedor);
            await unitOfWork.SaveAsync();
            if (proveedor== null)
            {
                return BadRequest();
            }

            proveedor.Id = proveedor.Id;
            return CreatedAtAction(nameof(Post), new { id = proveedor.Id }, proveedorDto);
        }   

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody] ProveedorDto proveedorDto)
        {
            if (proveedorDto == null)
            {
                return BadRequest();
            }
            var proveedor = mapper.Map<Proveedor>(proveedorDto);
            unitOfWork.Proveedores.Update(proveedor);
            await unitOfWork.SaveAsync();
            return proveedorDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await unitOfWork.Proveedores.GetByIdAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            unitOfWork.Proveedores.Remove(proveedor);
            await unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}