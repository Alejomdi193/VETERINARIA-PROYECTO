using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class LaboratorioDto
    {
        public int Id {get;set;}
        public string Nombre {get; set;}
        public string Correo {get; set;}
        public int Telefono {get; set;}
        public string Especialidad {get; set;}

    }
}