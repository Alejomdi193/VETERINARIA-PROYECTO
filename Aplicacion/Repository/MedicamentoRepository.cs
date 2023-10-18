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


    }
}