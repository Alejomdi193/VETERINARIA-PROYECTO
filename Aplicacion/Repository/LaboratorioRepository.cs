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
    public class LaboratorioRepository : GenericRepository<Laboratorio>, ILaboratorio
    {
        private readonly VeterinariaContext context;
        public LaboratorioRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<(int totalRegistros, IEnumerable<Laboratorio> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = context.Laboratorios as IQueryable<Laboratorio>;

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