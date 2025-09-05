using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Enums;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.ProcessoLogs;

public record CriarLogStatusCadastroCommand
{
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public bool IsEmailEnviado { get; set; }
    public int StatusCadastro { get; set; }
    public string? Pendencias { get; set; }
}
