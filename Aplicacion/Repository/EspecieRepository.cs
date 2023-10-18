using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class EspecieRepository : GenericRepository<Especie>, IEspecie
    {
        private VeterinariaContext context;
        public EspecieRepository(VeterinariaContext context) : base(context)
        {
            this.context = context;
        }


    }
}