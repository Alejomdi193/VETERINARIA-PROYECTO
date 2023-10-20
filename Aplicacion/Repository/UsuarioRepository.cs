using Dominio.Entidades;
using Dominio.Interface;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>,  IUserRepository
    {
        private readonly VeterinariaContext context;
        public UsuarioRepository(VeterinariaContext context) : base(context)
        {
            this.context= context;
        }

        public  async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
        {
            return await context.Usuarios
            .Include(p => p.Rols)
            .Include(p => p.RefreshTokens)
            .FirstOrDefaultAsync(p => p.RefreshTokens.Any(t => t.Token == refreshToken));
        }
         public override async Task<Usuario> GetByIdAsync(int id)
        {
            return await context.Usuarios
            .FirstOrDefaultAsync(p =>  p.Id == id);
        }
        public async Task<Usuario> GetByUserNameAsync(string Username)
        {
            return await context.Usuarios
            .Include(p => p.Rols)
            .Include(p => p.RefreshTokens)
            .FirstOrDefaultAsync(p => p.Nombre.ToLower() == Username.ToLower());
        }

    }
}