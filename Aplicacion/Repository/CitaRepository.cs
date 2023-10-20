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
            var cita = await context.Citas
            .Include(p => p.Mascota).ThenInclude(p => p.Propietario)
            .Include(p => p.Mascota).ThenInclude(p => p.Raza).ThenInclude(p => p.Especie)
            .Include(p => p.Veterinario)

            
            .ToListAsync();

            return cita;
        }

        public  async Task<IEnumerable<Cita>> ObtenerAnimales()
        {
            var cita = await context.Citas
            .Where(p => p.Fecha.Year == 2023)
            .Where(p => p.Motivo == "vacunacion")
            .Include(p => p.Mascota).ThenInclude(p => p.Propietario)
            .Include(p => p.Mascota).ThenInclude(p => p.Raza).ThenInclude(p => p.Especie)
            .Include(p => p.Veterinario)


            .ToListAsync();

            return cita;
        }

        public async Task<IEnumerable<Cita>> AnimalVeterinario(string nombre)
        {
            var cita = await context.Citas
            .Include(p => p.Mascota).ThenInclude(p => p.Propietario)
            .Include(p => p.Mascota).ThenInclude(p => p.Raza).ThenInclude(p => p.Especie)
            .Include(p => p.Veterinario)
            .Where(p => p.Veterinario.Nombre == nombre)

            .ToListAsync();

            return cita;
        }

        

        public override async Task<(int totalRegistros, IEnumerable<Cita> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
        {
        var query = context.Citas as IQueryable<Cita>;

        if (search != 0)
        {
            query = query.Where(p => p.IdVeterinarioFk == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Mascota)
            .Include(p => p.Veterinario)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
   
    }
}