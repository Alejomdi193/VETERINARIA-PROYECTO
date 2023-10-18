namespace Dominio.Entidades;
    public class TipoMovimiento: BaseEntity
    {
        public string Descripcion { get; set; }
        public ICollection<Movimiento> Movimientos {get; set;}
    }
