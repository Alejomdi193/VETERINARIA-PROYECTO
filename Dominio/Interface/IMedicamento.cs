using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IMedicamento : IGeneric<Medicamento>
    {
        Task<IEnumerable<Medicamento>> MedicamentoGenfar();
    }
}