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
    public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinario
    {
        private readonly VeterinariaContext context;



        public VeterinarioRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Veterinario>> VeterinariaCirujano()
        {
            var veterinarios = await context.Veterinarios
            .Where(p => p.Especialidad == "Cirujano Vascular")
            .ToListAsync();
            return veterinarios;
        }
        public override async Task<(int totalRegistros, IEnumerable<Veterinario> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
        {
            var query = context.Veterinarios as IQueryable<Veterinario>;

            if (!string.IsNullOrEmpty(search))
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