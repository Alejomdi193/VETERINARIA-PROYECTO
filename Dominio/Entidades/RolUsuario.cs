namespace Dominio.Entidades;
public class RolUsuario
{
    public int IdRolFk { get; set; }
    public Rol Rol { get; set; }
    public int IdUsuarioFk { get; set; }
    public Usuario Usuario { get; set; }
}