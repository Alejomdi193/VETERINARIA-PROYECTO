using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Helpers;
using Api.Services;
using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service
{
    public class UserService : IUserService
    {
         private readonly JWT _jwt;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Usuario> passwordHasher)
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var usuario = new Usuario
        {
            Email = registerDto.Email,
            Nombre = registerDto.Nombre
        };
        Console.WriteLine(registerDto.Nombre);
             Console.WriteLine(registerDto.Email);
                  Console.WriteLine(registerDto.Password);
        Console.WriteLine("a__________________");
        usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password); //Encrypt password

        var existingusuario = _unitOfWork.Usuarios
                                    .Find(u => u.Nombre.ToLower() == registerDto.Nombre.ToLower())
                                    .FirstOrDefault();

        if (existingusuario == null)
        {
                    Console.WriteLine("444444444");

            var rolDefault = _unitOfWork.Roles
                                    .Find(u => u.Nombre == Authorization.rol_default.ToString())
                                    .First();
            try
            {
                        Console.WriteLine("456456456");

                usuario.Rols.Add(rolDefault);
                                        Console.WriteLine("1");

                _unitOfWork.Usuarios.Add(usuario);
                                        Console.WriteLine("2");

                await _unitOfWork.SaveAsync();
                                        Console.WriteLine("33");

                return $"usuario  {registerDto.Nombre} has been registered successfully";
            }
            catch (Exception ex)
            {
                        Console.WriteLine("gjbbjbjbjbjbbj");

                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"usuario {registerDto.Nombre} already registered.";
        }
    }
    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto datausuarioDto = new DataUserDto();
        var usuario = await _unitOfWork.Usuarios
                    .GetByUserNameAsync(model.Nombre);

        if (usuario == null)
        {
            datausuarioDto.IsAuthenticated = false;
            datausuarioDto.Message = $"Usuario no Existe";
            return datausuarioDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            datausuarioDto.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            datausuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datausuarioDto.Email = usuario.Email;
            datausuarioDto.UserName = usuario.Nombre;
            datausuarioDto.Roles = usuario.Rols
                                            .Select(u => u.Nombre)
                                            .ToList();

            if (usuario.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = usuario.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                /* datausuarioDto.Message = "Usuario Existente"; */
                datausuarioDto.RefreshToken = activeRefreshToken.Token;
                datausuarioDto.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                datausuarioDto.RefreshToken = refreshToken.Token;
                datausuarioDto.RefreshTokenExpiration = refreshToken.Expires;
                usuario.RefreshTokens.Add(refreshToken);
                _unitOfWork.Usuarios.Update(usuario);
                await _unitOfWork.SaveAsync();
            }

            return datausuarioDto;
        }
        datausuarioDto.IsAuthenticated = false;
        datausuarioDto.Message = $"Credenciales incorrectas para el usuario";
        return datausuarioDto;
    }
    public async Task<string> AddRoleAsync(AddRoleDto model)
    {

        var usuario = await _unitOfWork.Usuarios
                    .GetByUserNameAsync(model.Nombre);
        if (usuario == null)
        {
            return $"usuario {model.Nombre} does not exists.";
        }

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            var rolExists = _unitOfWork.Roles
                                        .Find(u => u.Nombre.ToLower() == model.Role.ToLower())
                                        .FirstOrDefault();

            if (rolExists != null)
            {
                var usuarioHasRole = usuario.Rols
                                            .Any(u => u.Id == rolExists.Id);

                if (usuarioHasRole == false)
                {
                    usuario.Rols.Add(rolExists);
                    _unitOfWork.Usuarios.Update(usuario);
                    await _unitOfWork.SaveAsync();
                }

                return $"Role {model.Role} added to usuario {model.Nombre} successfully.";
            }

            return $"Role {model.Role} was not found.";
        }
        return $"Invalid Credentials";
    }
    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var dataUserDto = new DataUserDto();

        var usuario = await _unitOfWork.Usuarios
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Token is not assigned to any usuario.";
            return dataUserDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Token is not active.";
            return dataUserDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        dataUserDto.IsAuthenticated = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        dataUserDto.Email = usuario.Email;
        dataUserDto.UserName = usuario.Nombre;
        dataUserDto.Roles = usuario.Rols
                                        .Select(u => u.Nombre)
                                        .ToList();
        dataUserDto.RefreshToken = newRefreshToken.Token;
        dataUserDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return dataUserDto;
    }
    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }
    }
    private JwtSecurityToken CreateJwtToken(Usuario usuario)
    {
        var roles = usuario.Rols;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Nombre));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.Nombre),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
    }
}
        
    