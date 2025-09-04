using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Enums;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;

public class CedidoDto
{
    public int CedidoId { get; set; }
    public int TrabalhadorId { get; set; }
    public string? CnpjEmpregadoCedido { get; set; }
    public string? MatriculaTrabalhador { get; set; }
    public DateTime? DataAdmissao { get; set; }
    public TipoRegimeTrabalhista TipoRegTrab { get; set; }
    public TipoRegimePrevidenciario TipoRegPrev { get; set; }
    public OnusCessaoRequisicao OnusCessReq { get; set; }
    public CategoriaTrabalhador Categoria { get; set; }
    public Trabalhador? Trabalhador { get; set; }
}
