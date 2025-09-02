namespace Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;

public class Documento
{
    public int DocumentoId { get; set; }
    public string? Cpf { get; set; }
    public string? NisPisPasep { get; set; }
    public string? NumeroCtps { get; set; }
    public string? NumeroSerieCtps { get; set; }
    public string? UfCtps { get; set; }
    public string? NumeroRg { get; set; }
    public string? EmissaoRg { get; set; }
    public DateTime? DataExpedicaoRg { get; set; }
    public string? NumeroRegistroOc { get; set; }
    public string? EmissaoOc { get; set; }
    public DateTime? DataExpedOc { get; set; }
    public DateTime? DataValidadeOc { get; set; }
    public string? NumeroCnh { get; set; }
    public DateTime? DataExpedicaoCnh { get; set; }
    public string? UfCnh { get; set; }
    public DateTime? DataValidadeCnh { get; set; }
    public DateTime? DataPrimeiraHabilitacao { get; set; }
    public int? CategoriaCnhId { get; set; }
}
