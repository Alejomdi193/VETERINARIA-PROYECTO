namespace Api.Dtos
{
    public class TratamientoMedicoDto
    {
        public int Id {get; set;}
        public int Dosis {get; set;}
        public DateTime FechaAdministracion {get; set;}
        public string Observacion {get; set;}
        public CitaDto Cita {get; set;}
        public MedicamentoDto Medicamento {get; set;}
    }
}