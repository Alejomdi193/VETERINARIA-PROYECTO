namespace Api.Dtos
{
    public class MovimientoDto
    {
        public int Id {get;}
        public int Cantidad {get; set;}
        public int Precio {get; set;}
        public DateTime FechaMovimiento {get; set;}
        public TipoMovimientoDto TipoMovimiento{get; set;}
    }
}