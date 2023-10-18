using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class LaboratorioRepository : GenericRepository<Laboratorio>, ILaboratorio
    {
        public LaboratorioRepository(VeterinariaContext context) : base(context)
        {
        }
    }
}