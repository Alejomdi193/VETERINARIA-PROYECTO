using Dominio.Entidades;

namespace Dominio.Interface;
public interface IMascota : IGeneric<Mascota>
{
    Task<IEnumerable<Object>> ObtenerRazaFelina();
    Task<IEnumerable<Mascota>> PropietarioMascota();

    Task<IEnumerable<Object>> MascotaEspecie(string nombre);
    Task<IEnumerable<Mascota>> MascotaPropietario();
}
