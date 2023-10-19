using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Interface
{
    public interface IUsuario : IGeneric<Usuario>
    {
    Task<Usuario> GetByUsernameAsync(string username);
    Task<Usuario> GetByRefreshTokenAsync(string username);
        
    }
}