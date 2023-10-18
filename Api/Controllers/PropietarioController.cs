using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{

    public class PropietarioController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PropietarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<PropietarioDto>>> Get()
        {
            var propietarios = await unitOfWork.Propietarios.GetAllAsync();
            return mapper.Map<List<PropietarioDto>>(propietarios);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<PropietarioDto>> GetByIdAsync(int id)
        {
            var propietario = await unitOfWork.Propietarios.GetByIdAsync(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return mapper.Map<PropietarioDto>(propietario);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Propietario>> Post(PropietarioDto propietarioDto)
        {
            var propietario = mapper.Map<Propietario>(propietarioDto);
            unitOfWork.Propietarios.Add(propietario);
            await unitOfWork.SaveAsync();
            if (propietario == null)
            {
                return BadRequest();
            }

            propietario.Id = propietario.Id;
            return CreatedAtAction(nameof(Post), new { id = propietario.Id }, propietarioDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<PropietarioDto>> Put(int id, [FromBody] PropietarioDto propietarioDto)
        {
            if (propietarioDto == null)
            {
                return BadRequest();
            }
            var propietario = mapper.Map<Propietario>(propietarioDto);
            unitOfWork.Propietarios.Update(propietario);
            await unitOfWork.SaveAsync();
            return propietarioDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var propietarios = await unitOfWork.Propietarios.GetByIdAsync(id);
            if (propietarios == null)
            {
                return NotFound();
            }

            unitOfWork.Propietarios.Remove(propietarios);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}