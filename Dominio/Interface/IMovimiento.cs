using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IMovimiento : IGeneric<Movimiento>
    {
        Task<IEnumerable<object>> MedicamentoxVendedor();
    }
}