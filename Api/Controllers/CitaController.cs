using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

namespace Api.Controllers
{

    public class CitaController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CitaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork= unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<CitaDto>>> Get()
        {
            var cita = await unitOfWork.Citas.GetAllAsync();
            return mapper.Map<List<CitaDto>>(cita);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CitaDto>> Get(int id)
        {
            var cita = await unitOfWork.Citas.GetByIdAsync(id);

            if (cita == null)
            {
                return NotFound();
            }
            return mapper.Map<CitaDto>(cita);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Cita>> Post(CitaDto citaDto)
        {
            var cita = mapper.Map<Cita>(citaDto);
            unitOfWork.Citas.Add(cita);
            await unitOfWork.SaveAsync();
            if(cita == null)
            {
                return BadRequest();
            }
            cita.Id = cita.Id;
            return CreatedAtAction(nameof(Post), new {id = citaDto.Id},citaDto);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CitaDto>> Put(int id, [FromBody] CitaDto citaDto)
        {
            if(citaDto == null)
            {
                return BadRequest();
            }
        var cita = mapper.Map<Cita>(citaDto);
        unitOfWork.Citas.Update(cita);
        await unitOfWork.SaveAsync();
        return citaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var cita = await unitOfWork.Citas.GetByIdAsync(id);
            if (cita == null)
            {
                return BadRequest();
            }
            unitOfWork.Citas.Remove(cita);
            await unitOfWork.SaveAsync();
            return NoContent();
        }

        


    }
}