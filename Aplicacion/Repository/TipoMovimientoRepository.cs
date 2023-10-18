using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class TipoMovimientoRepository : GenericRepository<TipoMovimiento>, ITipoMovimiento
    {
        public TipoMovimientoRepository(VeterinariaContext context) : base(context)
        {
        }
    }
}