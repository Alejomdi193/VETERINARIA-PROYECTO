namespace Dominio.Entidades;
public class RefreshToken : BaseEntity
{
    public int IdUsuarioFk { get; set; }
    public Usuario Usuario { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Created { get; set; }
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
}