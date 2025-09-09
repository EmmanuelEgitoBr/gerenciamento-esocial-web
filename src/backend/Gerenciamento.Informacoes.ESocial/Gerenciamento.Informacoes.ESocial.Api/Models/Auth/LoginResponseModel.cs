﻿namespace Gerenciamento.Informacoes.ESocial.Api.Models.Auth;

public class LoginResponseModel
{
    public string? UserId { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
