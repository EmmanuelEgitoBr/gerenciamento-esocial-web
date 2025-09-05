using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogEnvioEmailCommand.CriarLogEnvioEmailCommand;

public class CriarLogEnvioEmailCommand : IRequest<ApiResponse<int>>
{
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public string? DescricaoLog { get; set; }
}
