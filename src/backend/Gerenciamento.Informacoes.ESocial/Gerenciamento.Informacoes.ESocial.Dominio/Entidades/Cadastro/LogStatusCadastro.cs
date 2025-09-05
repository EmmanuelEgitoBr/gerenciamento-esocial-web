using Gerenciamento.Informacoes.ESocial.Dominio.Enums;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;

public class LogStatusCadastro
{
    public int LogStatusCadastroId { get; set; }
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public bool IsEmailEnviado { get; set; }
    public StatusCadastro StatusCadastro { get; set; }
    public string? Pendencias { get; set; }
    public DateTime? DataEventoLog { get; set; }
}
