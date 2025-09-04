using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.CriarEstagiarioCommand;

public class CriarEstagiarioCommand : IRequest<ApiResponse<int>>
{
    public int TrabalhadorId { get; set; }
    public int NaturezaEstagio { get; set; }
    public int? AreaAtuacao { get; set; }
    public string? RazaoSocialInstEnsino { get; set; }
    public string? CnpjInstEnsino { get; set; }
    public string? NomeSupervisor { get; set; }
    public Endereco? EnderecoInstEnsino { get; set; }
}
