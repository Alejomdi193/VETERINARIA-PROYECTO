using Dominio.Entidades;

namespace Dominio.Interface;
    public interface IRaza : IGeneric<Raza>
    {
        Task<IEnumerable<object>> CantidadXRaza();
        
    }
