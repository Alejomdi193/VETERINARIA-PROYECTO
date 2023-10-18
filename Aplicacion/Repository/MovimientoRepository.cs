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
    public class MovimientoRepository : GenericRepository<Movimiento>, IMovimiento
    {
        private VeterinariaContext context;
        public MovimientoRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<Movimiento>> GetAllAsync()
        {
            var movimiento = await context.Movimientos
            .Include(p => p.TipoMovimiento)
            .ToListAsync();

            return movimiento;
        }
    }
}