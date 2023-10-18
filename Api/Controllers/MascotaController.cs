using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using AutoMapper;
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

        // [HttpGet("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]

        // public async Task<ActionResult<MascotaDto>> Get(int id)
        // {

        // }

    }
}