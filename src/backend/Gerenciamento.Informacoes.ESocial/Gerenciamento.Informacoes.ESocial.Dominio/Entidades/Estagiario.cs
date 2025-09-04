using Gerenciamento.Informacoes.ESocial.Dominio.Enums;
using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

public class Estagiario
{
    public int EstagiarioId { get; set; }
    public int TrabalhadorId { get; set; }
    public NaturezaEstagio NaturezaEstagio { get; set; }
    public AreaAtuacao AreaAtuacao { get; set; }
    public string? RazaoSocialInstEnsino { get; set; }
    public string? CnpjInstEnsino { get; set; }
    public string? NomeSupervisor { get; set; }
    public Endereco? EnderecoInstEnsino { get; set; }
    public Trabalhador? Trabalhador { get; set; }
}