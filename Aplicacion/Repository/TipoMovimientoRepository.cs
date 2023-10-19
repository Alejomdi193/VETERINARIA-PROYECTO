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
    public class TipoMovimientoRepository : GenericRepository<TipoMovimiento>, ITipoMovimiento
    {
        private readonly VeterinariaContext context;
        public TipoMovimientoRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<(int totalRegistros, IEnumerable<TipoMovimiento> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
        {
            var query = context.TipoMovimientos as IQueryable<TipoMovimiento>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Descripcion.ToLower().Contains(search));
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