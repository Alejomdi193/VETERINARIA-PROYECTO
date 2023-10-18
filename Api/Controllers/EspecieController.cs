using Api.Dtos;
using Aplicacion.UnitOfWork;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{

    public class EspecieController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EspecieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork= unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<EspecieDto>>> Get()
        {
            var especie = await unitOfWork.Especies.GetAllAsync();
            return mapper.Map<List<EspecieDto>>(especie);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EspecieDto>> Get(int id)
        {
            var especie = await unitOfWork.Especies.GetByIdAsync(id);
            if(especie == null)
            {
                return NotFound();
            }
            return mapper.Map<EspecieDto>(especie);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Especie>> Post(EspecieDto especieDto)
        {
            var especie = mapper.Map<Especie>(especieDto);
            unitOfWork.Especies.Add(especie);
            await unitOfWork.SaveAsync();
            if(especie == null)
            {
                return BadRequest();
            }
            especie.Id = especie.Id;
            return CreatedAtAction(nameof(Post),new{id = especieDto.Id}, especieDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EspecieDto>> Put(int id,[FromBody] EspecieDto especieDto)
        {
            if(especieDto == null)
            {
                return NotFound();
            }

            var especie = mapper.Map<Especie>(especieDto);
            unitOfWork.Especies.Add(especie);
            await unitOfWork.SaveAsync();
            return especieDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var especie = await unitOfWork.Especies.GetByIdAsync(id);
            if( especie == null)
            {
                return NotFound();
            }
            unitOfWork.Especies.Remove(especie);
            await unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}