using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;

namespace Dominio.Interfaces
{
    public interface IUserRepository : IGeneric<Usuario>
    {
        Task<Usuario> GetByUserNameAsync(string refreshToken);
        Task<Usuario> GetByRefreshTokenAsync(string username);
        Task<Usuario> GetByIdAsync(int id);
    }
}