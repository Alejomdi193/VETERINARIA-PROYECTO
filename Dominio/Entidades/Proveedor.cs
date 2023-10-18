namespace Dominio.Entidades;
    public class Proveedor : BaseEntity
    {
        public string Nombre {get; set;}
        public string Direccion {get; set;}
        public int Telefono {get; set;}

        public ICollection<MedicamentoProveedor> MedicamentoProveedores{get; set;}
        public ICollection<Medicamento> Medicamentos {get; set;}
        

    }
