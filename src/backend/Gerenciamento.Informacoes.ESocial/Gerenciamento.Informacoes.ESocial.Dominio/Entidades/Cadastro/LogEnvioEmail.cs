namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;

public class LogEnvioEmail
{
    public int LogEnvioEmailId { get; set; }
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public string? DescricaoLog { get; set; }
    public DateTime? DataEventoLog { get; set; }
}
