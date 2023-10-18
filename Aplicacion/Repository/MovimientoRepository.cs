using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MovimientoRepository : GenericRepository<Movimiento>, IMovimiento
    {
        public MovimientoRepository(VeterinariaContext context) : base(context)
        {
        }
    }
}