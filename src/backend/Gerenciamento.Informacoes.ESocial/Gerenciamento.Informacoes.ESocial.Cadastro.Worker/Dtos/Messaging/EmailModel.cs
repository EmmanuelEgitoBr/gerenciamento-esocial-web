namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Messaging;

public record EmailModel
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
