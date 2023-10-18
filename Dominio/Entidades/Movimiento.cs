

namespace Dominio.Entidades
{
    public class Movimiento : BaseEntity
    {
        public int Cantidad {get; set;}
        public int Precio {get; set;}
        public DateTime FechaMovimiento {get; set;}
        public int IdTipoMovimientoFk {get; set;}
        public TipoMovimiento TipoMovimiento{get; set;}
        public ICollection<MovimientoMedicamento> MovimientoMedicamentos {get; set;}
        public ICollection<Medicamento> Medicamentos {get; set;}

    }
}