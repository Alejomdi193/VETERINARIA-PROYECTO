using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class PropietarioRepository : GenericRepository<Propietario>, IPropietario
    {
        private VeterinariaContext context;
        public PropietarioRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<(int totalRegistros, IEnumerable<Propietario> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = context.Propietarios as IQueryable<Propietario>;

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