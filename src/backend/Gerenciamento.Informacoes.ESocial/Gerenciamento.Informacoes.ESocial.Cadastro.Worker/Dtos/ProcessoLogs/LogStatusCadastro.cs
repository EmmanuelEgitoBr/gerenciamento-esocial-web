using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Enums;
using System.Text.Json.Serialization;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.ProcessoLogs;

public record LogStatusCadastro
{

    [JsonPropertyName("trabalhadorId")]
    public int TrabalhadorId { get; set; }

    [JsonPropertyName("emailTrabalhador")]
    public string? EmailTrabalhador { get; set; }

    [JsonPropertyName("isEmailEnviado")]
    public bool IsEmailEnviado { get; set; }

    [JsonPropertyName("statusCadastro")]
    public StatusCadastro StatusCadastro { get; set; }

    [JsonPropertyName("pendencias")]
    public string? Pendencias { get; set; }
}
