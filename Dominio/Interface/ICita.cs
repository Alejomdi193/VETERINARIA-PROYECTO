using Dominio.Entidades;

namespace Dominio.Interface;
public interface ICita : IGeneric<Cita>
{
   Task<IEnumerable<Cita>> ObtenerAnimales();
   Task<IEnumerable<Cita>> AnimalVeterinario(string nombre);
}
