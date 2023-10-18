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

    }

}