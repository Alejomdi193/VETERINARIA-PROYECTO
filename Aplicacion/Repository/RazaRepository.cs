using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class RazaRepository : GenericRepository<Raza>, IRaza
    {
        public RazaRepository(VeterinariaContext context) : base(context)
        {
        }
    }
}