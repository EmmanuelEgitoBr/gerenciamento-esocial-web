namespace Gerenciamento.Informacoes.ESocial.Api.Models.Auth;

public class UserResponseModel
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public IList<string>? Roles { get; set; }
}
