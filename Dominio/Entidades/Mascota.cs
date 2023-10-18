namespace Dominio.Entidades;
    public class Mascota : BaseEntity
    {
        public string Nombre {get; set;}
        public DateTime FechaNac {get; set;}
        public int IdPropietarioFk {get; set;}
        public Propietario Propietario {get; set;}
        public int IdRazaFk {get; set;}
        public Raza Raza {get; set;}
        public ICollection<Cita> Citas {get; set;}
    }
