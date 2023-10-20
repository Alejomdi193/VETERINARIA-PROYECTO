using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
    {
        private readonly VeterinariaContext context;
        public ProveedorRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Proveedor>> ProveedoresxMedicamento(string nombre)
        {
            var proveedor = await context.Proveedores
            .Where(p => p.Medicamentos.Any(m => m.Nombre == nombre))
            .Include(p => p.Medicamentos)
            .ToListAsync();

            return proveedor;
        }


        public override async Task<(int totalRegistros, IEnumerable<Proveedor> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
        {
            var query = context.Proveedores as IQueryable<Proveedor>;

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