using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class MedicamentoDto
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Stock {get; set;}
        public double Precio {get; set;}
        public LaboratorioDto Laboratorio {get; set;}

    }
}