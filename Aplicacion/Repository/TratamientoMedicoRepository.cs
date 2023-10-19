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
    public class TratamientoMedicoRepository : GenericRepository<TratamientoMedico>, ITratamientoMedico
    {
        private VeterinariaContext context;
        public TratamientoMedicoRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<IEnumerable<TratamientoMedico>> GetAllAsync()
        {
            var tratamientoMedico = await context.TratamientoMedicos
            .Include(p => p .Cita).ThenInclude(p => p.Mascota).ThenInclude(p => p.Raza)
            .Include(p => p .Cita).ThenInclude(p => p.Mascota).ThenInclude(p => p.Propietario)
            .Include(p => p .Cita).ThenInclude(p => p.Mascota).ThenInclude(p => p.Raza).ThenInclude(p => p.Especie)
            .Include(p => p .Cita).ThenInclude(p => p.Veterinario)
            .Include(p => p.Medicamento).ThenInclude(p => p.Laboratorio)
            .ToListAsync();

            return tratamientoMedico;
        }

        public override async Task<(int totalRegistros, IEnumerable<TratamientoMedico> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
        {
        var query = context.TratamientoMedicos as IQueryable<TratamientoMedico>;

        if (search != 0)
        {
            query = query.Where(p => p.IdCitasFk == search);
            query = query.Where(p => p.IdMedicamentoFk == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Medicamento)
            .Include(p => p.Cita)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}