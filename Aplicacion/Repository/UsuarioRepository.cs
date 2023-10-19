using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>,  IUsuario
    {
        public UsuarioRepository(VeterinariaContext context) : base(context)
        {
        }

    }
}