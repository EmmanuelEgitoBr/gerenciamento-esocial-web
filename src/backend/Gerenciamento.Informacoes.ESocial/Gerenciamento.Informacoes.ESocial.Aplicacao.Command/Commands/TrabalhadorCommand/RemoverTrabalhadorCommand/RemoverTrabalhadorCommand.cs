using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.RemoverTrabalhadorCommand;

public class RemoverTrabalhadorCommand : IRequest<ApiResponse<int>>
{
    public int TrabalhadorId { get; set; }
}
