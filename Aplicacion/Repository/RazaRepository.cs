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

        public override async Task<(int totalRegistros, IEnumerable<Raza> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
        {
        var query = context.Razas as IQueryable<Raza>;

        if (search != 0)
        {
            query = query.Where(p => p.IdEspecieFk == search);

        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Especie)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}