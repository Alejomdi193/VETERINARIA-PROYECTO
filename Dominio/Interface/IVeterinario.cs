
using Dominio.Entidades;

namespace Dominio.Interface;
    public interface IVeterinario : IGeneric<Veterinario>
    {
        Task<IEnumerable<Veterinario>> VeterinariaCirujano();
    }
