using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
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
        [MapToApiVersion("1.0")]
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
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MascotaDto>>> GetPagination([FromQuery] Params paisParams)
        {
            var entidad = await unitOfWork.Mascotas.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
            var listEntidad = mapper.Map<List<MascotaDto>>(entidad.registros);
            return new Pager<MascotaDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        }


        [HttpGet("razaFelina")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<Object>>> Get3()
        {
            var mascota = await unitOfWork.Mascotas.ObtenerRazaFelina();

            if(mascota == null)
            {
                return NotFound();
            }
            return mapper.Map<List<Object>>(mascota);
        }

        [HttpGet("propietarioMascota")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MascotaDto>>> Get4()
        {
            var mascotas = await unitOfWork.Mascotas.PropietarioMascota();
            if (mascotas == null)
            {
                return NotFound();
            }
            return mapper.Map<List<MascotaDto>>(mascotas);
        }

        [HttpGet("mascotaxEspecie/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<Object>>> Get7(string nombre)
        {
            var mascotas = await unitOfWork.Mascotas.MascotaEspecie(nombre);
            if (mascotas == null)
            {
                return NotFound();
            }
            return mapper.Map<List<Object>>(mascotas);
        }


        [HttpGet("mascotaPropietario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MascotaDto>>> Get11()
        {
            var mascotas = await unitOfWork.Mascotas.MascotaPropietario();
            if (mascotas == null)
            {
                return NotFound();
            }
            return mapper.Map<List<MascotaDto>>(mascotas);
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