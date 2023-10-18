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
        private VeterinariaContext _context;
        public MascotaRepository(VeterinariaContext context) : base(context)
        {
            _context=context;
        }

       
        public override async Task<IEnumerable<Mascota>> GetAllAsync()
        {
            var mascota =  await _context.Mascotas
            .Include(t=>t.Propietario)
            .Include(e=>e.Raza)
            .ThenInclude(p => p.Especie)
            .ToListAsync();

            return mascota;
        }

    }
}