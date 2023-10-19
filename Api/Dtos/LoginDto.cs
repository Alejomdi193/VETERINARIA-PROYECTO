using System.ComponentModel.DataAnnotations;


namespace Api.Dtos;
public class LoginDto
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Password { get; set; }     
}