using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class CitaRepository : GenericRepository<Cita>, ICita
    {
        public CitaRepository(VeterinariaContext context) : base(context)
        {
            
        }
    }
}