
using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Api.Helpers.Errors;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
[ApiVersion("1.0")]
[ApiVersion("1.1")]


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
        [MapToApiVersion("1.0")]
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


        [HttpGet("1.1")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VeterinarioDto>>> GetPagination([FromQuery] Params veterinarioParams)
        {
            var entidad = await unitOfWork.Veterinarios.GetAllAsync(veterinarioParams.PageIndex, veterinarioParams.PageSize, veterinarioParams.Search);
            var listEntidad = mapper.Map<List<VeterinarioDto>>(entidad.registros);
            return new Pager<VeterinarioDto>(listEntidad, entidad.totalRegistros, veterinarioParams.PageIndex, veterinarioParams.PageSize, veterinarioParams.Search);
        } 

        //Consulta 1

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Veterinario>> Post(VeterinarioDto veterinarioDto)
        {
            var veterinario = mapper.Map<Veterinario>(veterinarioDto);
            unitOfWork.Veterinarios.Add(veterinario);
            await unitOfWork.SaveAsync();
            if (veterinario== null)
            {
                return BadRequest();
            }

            veterinario.Id = veterinario.Id;
            return CreatedAtAction(nameof(Post), new { id = veterinario.Id }, veterinarioDto);
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