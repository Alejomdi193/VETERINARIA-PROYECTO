
using Api.Dtos;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{

    public class VeterinarioController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VeterinarioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<VeterinarioDto>>> Get()
        {
            var veterinario = await unitOfWork.Veterinarios.GetAllAsync();
            return mapper.Map<List<VeterinarioDto>>(veterinario);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<VeterinarioDto>> GetByIdAsync(int id)
        {
            var veterinario = await unitOfWork.Veterinarios.GetByIdAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return mapper.Map<VeterinarioDto>(veterinario);
        }  

        [HttpGet("cirujanoespecialidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<VeterinarioDto>>> Get1 ()
        {
            var veterinario = await unitOfWork.Veterinarios.VeterinariaCirujano();
            if (veterinario == null)
            {
                return NotFound();
            }
            return mapper.Map<List<VeterinarioDto>>(veterinario);
        }  


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VeterinarioDto>> Put(int id, [FromBody]VeterinarioDto veterinarioDto)
        {
            if (veterinarioDto == null)
            {
                return BadRequest();
            }
            var veterinario = mapper.Map<Veterinario>(veterinarioDto);
            unitOfWork.Veterinarios.Update(veterinario);
            await unitOfWork.SaveAsync();
            return veterinarioDto;
        } 

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var veterinario = await unitOfWork.Veterinarios.GetByIdAsync(id);
            if (veterinario== null)
            {
                return NotFound();
            }

            unitOfWork.Veterinarios.Remove(veterinario);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}