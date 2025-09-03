﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gerenciamento.Informacoes.ESocial.Api.Models.Auth;

public class RegisterModel
{
    [JsonPropertyName("username")]
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Cpf is required")]
    public string? Cpf { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
