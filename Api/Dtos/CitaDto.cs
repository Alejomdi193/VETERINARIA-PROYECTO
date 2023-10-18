using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class CitaDto
    {
        public int Id {get; set;}
        public DateTime Fecha {get; set; }
        public string Motivo {get; set;}
        public MascotaDto Mascota {get; set;}
        public VeterinarioDto Veterinario {get; set;}
    }
}