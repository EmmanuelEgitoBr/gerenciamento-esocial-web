namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;

public class CedidoDto
{
    public int CedidoId { get; set; }
    public int TrabalhadorId { get; set; }
    public string? CnpjEmpregadoCedido { get; set; }
    public string? MatriculaTrabalhador { get; set; }
    public DateTime? DataAdmissao { get; set; }
    public int TipoRegTrab { get; set; }
    public int TipoRegPrev { get; set; }
    public int OnusCessReqId { get; set; }
    public int? CategoriaId { get; set; }
    public TrabalhadorDto? Trabalhador { get; set; }
}
