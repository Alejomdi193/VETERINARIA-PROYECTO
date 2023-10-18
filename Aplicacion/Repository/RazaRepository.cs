using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class RazaRepository : GenericRepository<Raza>, IRaza
    {
        private VeterinariaContext context;
        public RazaRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<Raza>> GetAllAsync()
        {
            var raza = await context.Razas
            .Include(p => p.Especie)
            .ToListAsync();

            return raza;
        }
    }
}