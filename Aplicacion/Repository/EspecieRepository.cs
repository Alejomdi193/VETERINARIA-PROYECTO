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
    public class EspecieRepository : GenericRepository<Especie>, IEspecie
    {
        private VeterinariaContext context;
        public EspecieRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<(int totalRegistros, IEnumerable<Especie> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = context.Especies as IQueryable<Especie>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}