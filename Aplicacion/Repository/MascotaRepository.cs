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

        public async Task<IEnumerable<Mascota>> ObtenerRazaFelina()
        {
            var mascota = await context.Mascotas
            .Include(p => p.Raza).ThenInclude(p => p.Especie)
            .Where(p => p.Raza.Nombre == "Felina")
            .ToListAsync();

            return mascota;
        }

    }
}