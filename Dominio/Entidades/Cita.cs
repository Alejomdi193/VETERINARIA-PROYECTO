namespace Dominio.Entidades;
    public class Cita : BaseEntity
    {
        public DateTime Fecha {get; set;}
        public string Motivo {get; set;}
        public int IdMascotaFk {get; set;}
        public Mascota Mascota {get; set;}
        public int IdVeterinarioFk {get; set;}
        public Veterinario Veterinario {get; set;}
        public ICollection<TratamientoMedico> TratamientoMedicos {get; set;}
        
    }
