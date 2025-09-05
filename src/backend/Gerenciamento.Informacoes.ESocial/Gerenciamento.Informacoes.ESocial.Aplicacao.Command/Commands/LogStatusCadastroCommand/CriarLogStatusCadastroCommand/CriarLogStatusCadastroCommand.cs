using Gerenciamento.Informacoes.ESocial.Dominio.Enums;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogStatusCadastroCommand.CriarLogStatusCadastroCommand;

public class CriarLogStatusCadastroCommand : IRequest<ApiResponse<int>>
{
    public int TrabalhadorId { get; set; }
    public string? EmailTrabalhador { get; set; }
    public bool IsEmailEnviado { get; set; }
    public StatusCadastro StatusCadastro { get; set; }
    public string? Pendencias { get; set; }
}
