using Dominio.Entidades;

namespace Dominio.Interface;
    public interface IProveedor : IGeneric<Proveedor>
    { 
        Task<IEnumerable<Proveedor>> ProveedoresxMedicamento(string nombre);
    }
