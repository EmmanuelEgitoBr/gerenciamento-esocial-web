using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Enums;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Messaging;

public record MessageModel
{
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public StatusCadastro StatusCadastro { get; set; }
    public string? Pendencias { get; set; }
    public EmailModel? Email { get; set; }
}
