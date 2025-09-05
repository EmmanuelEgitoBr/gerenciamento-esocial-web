namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.ProcessoLogs;

public record CriarLogEnvioEmailCommand
{    
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public string? DescricaoLog { get; set; }
}
