using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class RazaDto
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public EspecieDto Especie {get; set;}
    }
}