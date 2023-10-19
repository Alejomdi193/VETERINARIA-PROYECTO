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
    public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
    {
        private readonly VeterinariaContext context;
        public MedicamentoRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Medicamento>> MedicamentoGenfar()
        {
            var medicamento = await context.Medicamentos
            .Where(p => p.Laboratorio.Nombre == "Genfar")
            .ToListAsync();

            return medicamento;
        }

        public override async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            var medicamento = await context.Medicamentos
            .Include(p => p.Laboratorio)
            .ToListAsync();

            return medicamento;
        }

        public async Task<IEnumerable<Medicamento>> VentaMayor()
        {
            var medicamento = await context.Medicamentos
            .Where(p => p.Precio > 50000)
            .Include(p => p.Laboratorio)
            .ToListAsync(); 

            return medicamento;
        }

        public override async Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
        {
        var query = context.Medicamentos as IQueryable<Medicamento>;

        if (search != 0)
        {
            query = query.Where(p => p.IdLaboratorioFk == search);

        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Laboratorio)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }


    }
}