using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class MascotaDto
    {
        public string Nombre {get; set;}
        public DateTime FechaNac {get; set;}
        public PropietarioDto Propietario{get; set;}
        public RazaDto Raza {get; set;}
    }
}