using Gerenciamento.Informacoes.ESocial.Dominio.Enums;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Models;

public record MessageModel
{
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public bool IsEmailRecebido { get; set; }
    public StatusCadastro StatusCadastro { get; set; }
    public string? Pendencias { get; set; }
    public EmailModel? Email { get; set; }
}
