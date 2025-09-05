using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Enums;
using System.Text.Json.Serialization;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Messaging;

public record MessageModel
{
    [JsonPropertyName("trabalhadorId")]
    public int TrabalhadorId { get; set; }

    [JsonPropertyName("emailTrabalhador")]
    public string? EmailTrabalhador { get; set; }

    [JsonPropertyName("statusCadastro")]
    public StatusCadastro StatusCadastro { get; set; }

    [JsonPropertyName("pendencias")]
    public string? Pendencias { get; set; }

    [JsonPropertyName("email")]
    public EmailModel? Email { get; set; }
}
