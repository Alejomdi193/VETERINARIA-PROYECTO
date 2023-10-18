using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class CitaRepository : GenericRepository<Cita>, ICita
    {
        private VeterinariaContext context;
        public CitaRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
            
        }

        public override async Task<IEnumerable<Cita>> GetAllAsync()
        {
            var especie = await context.Citas
            .Include(p => p.Mascota).ThenInclude(p => p.Propietario)
            .Include(p => p.Mascota).ThenInclude(p => p.Raza).ThenInclude(p => p.Especie)
            .Include(p => p.Veterinario)

            
            .ToListAsync();

            return especie;
        }
    }
}