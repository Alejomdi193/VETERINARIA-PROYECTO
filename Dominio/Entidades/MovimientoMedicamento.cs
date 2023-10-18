namespace Dominio.Entidades
{
    public class MovimientoMedicamento
    {
        public int IdMedicamentoFk {get; set;}
        public Medicamento Medicamento {get; set;}
        public int IdMovimientoFk {get; set;}
        public Movimiento Movimiento {get; set;}
    }
}