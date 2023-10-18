namespace Dominio.Entidades
{
    public class MedicamentoProveedor 
    {

        public int IdProveedorFk {get; set;}
        public Proveedor Proveedor {get; set;}
        public int IdMedicamentoFk {get; set;}
        public Medicamento Medicamento {get; set;}
        
    }
}