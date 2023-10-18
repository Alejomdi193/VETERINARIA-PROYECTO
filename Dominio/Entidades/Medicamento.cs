namespace Dominio.Entidades;
    public class Medicamento : BaseEntity
    {
        public string Nombre {get; set;}
        public int Stock {get; set;}
        public double Precio {get; set;}
        public int IdLaboratorioFk {get;set;}
        public Laboratorio Laboratorio{get; set;}

        public ICollection<MovimientoMedicamento> MovimientoMedicamentos{get; set;}
        public ICollection<MedicamentoProveedor> MedicamentoProveedores {get; set;}
        public ICollection<TratamientoMedico> TratamientoMedicos {get; set;}

        public ICollection<Proveedor> Proveedores {get; set;}
        public ICollection<Movimiento> Movimientos { get; set; }

        

    }
