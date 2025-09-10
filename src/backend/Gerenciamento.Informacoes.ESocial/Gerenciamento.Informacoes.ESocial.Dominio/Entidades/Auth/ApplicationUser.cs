using Microsoft.AspNetCore.Identity;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Auth;

public class ApplicationUser : IdentityUser
{
    public string? Cpf { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
