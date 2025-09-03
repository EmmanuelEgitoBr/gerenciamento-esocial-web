﻿using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.AtualizarCedidoCommand;

public class AtualizarCedidoCommand : IRequest<ApiResponse<int>>
{
    public int TrabalhadorId { get; set; }
    public string? CnpjEmpregadoCedido { get; set; }
    public string? MatriculaTrabalhador { get; set; }
    public DateTime? DataAdmissao { get; set; }
    public int TipoRegTrab { get; set; }
    public int TipoRegPrev { get; set; }
    public int OnusCessReqId { get; set; }
    public int? CategoriaId { get; set; }
}
