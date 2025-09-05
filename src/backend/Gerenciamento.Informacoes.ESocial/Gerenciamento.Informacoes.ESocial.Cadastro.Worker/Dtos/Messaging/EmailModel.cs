using System.Text.Json.Serialization;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Messaging;

public record EmailModel
{
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
