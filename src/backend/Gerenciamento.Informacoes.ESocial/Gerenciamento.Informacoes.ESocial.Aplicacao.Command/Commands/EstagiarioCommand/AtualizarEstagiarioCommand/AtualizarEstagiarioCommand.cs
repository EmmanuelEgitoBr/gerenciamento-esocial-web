using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.AtualizarEstagiarioCommand;

public class AtualizarEstagiarioCommand : IRequest<ApiResponse<int>>
{
    public int TrabalhadorId { get; set; }
    public int NaturezaEstagio { get; set; }
    public int? AreaAtuacaoId { get; set; }
    public string? RazaoSocialInstEnsino { get; set; }
    public string? CnpjInstEnsino { get; set; }
    public string? NomeSupervisor { get; set; }
    public Endereco? EnderecoInstEnsino { get; set; }
}
