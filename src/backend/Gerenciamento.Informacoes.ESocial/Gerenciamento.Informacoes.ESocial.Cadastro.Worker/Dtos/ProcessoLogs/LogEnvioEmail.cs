using System.Text.Json.Serialization;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.ProcessoLogs;

public record LogEnvioEmail
{    
    [JsonPropertyName("trabalhadorId")]
    public int TrabalhadorId { get; set; }

    [JsonPropertyName("emailTrabalhador")]
    public string? EmailTrabalhador { get; set; }

    [JsonPropertyName("descricaoLog")]
    public string? DescricaoLog { get; set; }
}
