﻿using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.RemoverCedidoCommand;

public class RemoverCedidoCommand : IRequest<ApiResponse<int>>
{
    public int CedidoId { get; set; }
}
