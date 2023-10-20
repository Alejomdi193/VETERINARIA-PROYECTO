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

        public async Task<IEnumerable<object>> MedicamentoxVendedor()
        {
            var movimiento = await context.Movimientos
            .Include(p => p.MovimientoMedicamentos)
            .ThenInclude(p => p.Medicamento)
            .ToListAsync();

            var resultado = movimiento.Select(m =>
            new 
            {
                IdMovimiento = m.Id,
                Cantida = m.Cantidad,
                Preci = m.Precio,
                ValorTotal = m.Cantidad * m.Precio,
                Medicamento = m.MovimientoMedicamentos.Select(m => m.Medicamento.Nombre)
            });

            return resultado;
        }

        public override async Task<(int totalRegistros, IEnumerable<Movimiento> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
        {
        var query = context.Movimientos as IQueryable<Movimiento>;

        if (search != 0)
        {
            query = query.Where(p => p.IdTipoMovimientoFk == search);

        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.TipoMovimiento)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}