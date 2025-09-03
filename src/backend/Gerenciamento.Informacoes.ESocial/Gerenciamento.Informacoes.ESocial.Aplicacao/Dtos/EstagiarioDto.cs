using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Dtos;

public class EstagiarioDto
{
    public int EstagiarioId { get; set; }
    public int TrabalhadorId { get; set; }
    public int NaturezaEstagio { get; set; }
    public int? AreaAtuacaoId { get; set; }
    public string? RazaoSocialInstEnsino { get; set; }
    public string? CnpjInstEnsino { get; set; }
    public string? NomeSupervisor { get; set; }
    public Endereco? EnderecoInstEnsino { get; set; }
    public TrabalhadorDto? Trabalhador { get; set; }
}
