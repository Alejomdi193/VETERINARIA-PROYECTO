using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class TratamientoMedicoRepository : GenericRepository<TratamientoMedico>, ITratamientoMedico
    {
        public TratamientoMedicoRepository(VeterinariaContext context) : base(context)
        {
        }
    }
}