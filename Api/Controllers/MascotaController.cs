using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class MascotaController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MascotaController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MascotaDto>>> Get()
        {
            var mascotas = await unitOfWork.Mascotas.GetAllAsync();
            return mapper.Map<List<MascotaDto>>(mascotas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MascotaDto>> Get(int id)
        {
            var mascota = await unitOfWork.Mascotas.GetByIdAsync(id);

            if(mascota == null)
            {
                return NotFound();
            }
            return mapper.Map<MascotaDto>(mascota);
        }


        [HttpGet("razaFelina")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MascotaDto>> Get3()
        {
            var mascota = await unitOfWork.Mascotas.ObtenerRazaFelina();

            if(mascota == null)
            {
                return NotFound();
            }
            return mapper.Map<MascotaDto>(mascota);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Mascota>> Post(MascotaDto mascotaDto)
        {
            var mascota = mapper.Map<Mascota>(mascotaDto);
            unitOfWork.Mascotas.Add(mascota);
            await unitOfWork.SaveAsync();
            if(mascota == null)
            {
                return BadRequest();
            }
            mascota.Id = mascota.Id;
            return CreatedAtAction(nameof(Post),new {id = mascota.Id}, mascotaDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MascotaDto>> Put (int id, [FromBody] MascotaDto mascotaDto)
        {
            if(mascotaDto == null)
            {
                return NotFound();
            }
            var mascota = mapper.Map<Mascota>(mascotaDto);
            unitOfWork.Mascotas.Update(mascota);
            await unitOfWork.SaveAsync();
            return mascotaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Delete(int id)
        {
            var mascota = await unitOfWork.Mascotas.GetByIdAsync(id);
            if(mascota == null)
            {
                return BadRequest();
            }
            unitOfWork.Mascotas.Remove(mascota);
            await unitOfWork.SaveAsync();
            return NoContent();

        }

    }
}