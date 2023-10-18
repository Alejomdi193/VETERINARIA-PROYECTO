using AutoMapper;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;

namespace Api.Controllers
{

    public class MovimientoController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MovimientoDto>>> Get()
        {
            var movimiento = await unitOfWork.Movimientos.GetAllAsync();
            return mapper.Map<List<MovimientoDto>>(movimiento);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MovimientoDto>> GetByIdAsync(int id)
        {
            var movimiento = await unitOfWork.Movimientos.GetByIdAsync(id);
            if( movimiento == null)
            {
                return NotFound();
            }
            return mapper.Map<MovimientoDto>(movimiento);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Movimiento>> Post(MovimientoDto movimientoDto)
        {
            var movimiento = mapper.Map<Movimiento>(movimientoDto);
            unitOfWork.Movimientos.Add(movimiento);
            await unitOfWork.SaveAsync();
            if(movimiento == null)
            {
                return BadRequest();
            }

            movimiento.Id = movimiento.Id;
            return CreatedAtAction(nameof(Post), new{id = movimiento.Id},movimientoDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MovimientoDto>> Put (int id, [FromBody] MovimientoDto movimientoDto)
        {
                if(movimientoDto == null)
                {
                    return BadRequest();
                }
                var movimiento = mapper.Map<Movimiento>(movimientoDto);
                unitOfWork.Movimientos.Update(movimiento);
                await unitOfWork.SaveAsync();
                return movimientoDto;
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id)
        {
            var movimiento = await unitOfWork.Movimientos.GetByIdAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            unitOfWork.Movimientos.Remove(movimiento);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}