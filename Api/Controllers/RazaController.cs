using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class RazaController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
        {
            var raza = await unitOfWork.Razas.GetAllAsync();
            return mapper.Map<List<RazaDto>>(raza);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<RazaDto>> GetByIdAsync(int id)
        {
            var raza = await unitOfWork.Razas.GetByIdAsync(id);
            if (raza == null)
            {
                return NotFound();
            }
            return mapper.Map<RazaDto>(raza);
        }  

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Raza>> Post(RazaDto razaDto)
        {
            var raza = mapper.Map<Raza>(razaDto);
            unitOfWork.Razas.Add(raza);
            await unitOfWork.SaveAsync();
            if (raza== null)
            {
                return BadRequest();
            }

            raza.Id = raza.Id;
            return CreatedAtAction(nameof(Post), new { id = raza.Id }, razaDto);
        }  

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<RazaDto>> Put(int id, [FromBody] RazaDto razaDto)
        {
            if (razaDto == null)
            {
                return BadRequest();
            }
            var raza = mapper.Map<Raza>(razaDto);
            unitOfWork.Razas.Update(raza);
            await unitOfWork.SaveAsync();
            return razaDto;
        } 

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var raza = await unitOfWork.Razas.GetByIdAsync(id);
            if (raza == null)
            {
                return NotFound();
            }

            unitOfWork.Razas.Remove(raza);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}