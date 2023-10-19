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
    public class MascotaRepository : GenericRepository<Mascota>, IMascota
    {
        private  readonly VeterinariaContext context;
        public MascotaRepository(VeterinariaContext context) : base(context)
        {
            this.context=context;
        }

       
        public override async Task<IEnumerable<Mascota>> GetAllAsync()
        {
            var mascota =  await context.Mascotas
            .Include(t=>t.Propietario)
            .Include(e=>e.Raza)
            .ThenInclude(p => p.Especie)
            .ToListAsync();

            return mascota;
        }

        public async Task<IEnumerable<Object>> ObtenerRazaFelina()
        {
            return await
            (
                from m in context.Mascotas
                join r in context.Razas on m.IdRazaFk equals r.Id
                join e in context.Especies on r.IdEspecieFk equals e.Id
                where e.Nombre.Contains("Felina")
                select new 
                {
                    Nombre = m.Nombre,
                    Fecha = m.FechaNac,
                    Especie = e.Nombre
                }
                
                
            ).ToListAsync();
            
        }

        public async Task<IEnumerable<Mascota>> PropietarioMascota()
        {
           var mascota = await context.Mascotas
            .Include(p => p.Raza)
            .ThenInclude(p => p.Especie)
            .Include(p => p.Propietario)
            
            .ToListAsync();

            return mascota;

           
        }

        public async Task<IEnumerable<Object>> MascotaEspecie(string nombre)
        {
            return await
            (
                from m in context.Mascotas
                join r in context.Razas on m.IdRazaFk equals r.Id
                join e in context.Especies on r.IdEspecieFk equals e.Id
                where e.Nombre.Contains(nombre)
                select new 
                {
                    Nombre = m.Nombre,
                    Fecha = m.FechaNac,
                    Especie = e.Nombre
                }
                
                
            ).ToListAsync();

        }

        public async Task<IEnumerable<Mascota>> MascotaPropietario()
        {
            var mascota = await context.Mascotas
            .Include(p => p.Raza).ThenInclude(p => p.Especie)
            .Include(p => p.Propietario)
            .Where(p => p.Raza.Nombre == "Golden Retriver")
            .ToListAsync();

            return mascota;
        }

        public override async Task<(int totalRegistros, IEnumerable<Mascota> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
        {
        var query = context.Mascotas as IQueryable<Mascota>;

        if (search != 0)
        {
            query = query.Where(p => p.IdPropietarioFk == search);
            query = query.Where(p => p.IdRazaFk == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Raza)
            .Include(p => p.Propietario)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    }
}