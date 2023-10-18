namespace Api.Dtos
{
    public class TratamientoMedicoDto
    {
        public int Dosis {get; set;}
        public DateTime FechaAdministracion {get; set;}
        public string Observacion {get; set;}
        public CitaDto Citas {get; set;}
        public MedicamentoDto Medicamento {get; set;}
    }
}