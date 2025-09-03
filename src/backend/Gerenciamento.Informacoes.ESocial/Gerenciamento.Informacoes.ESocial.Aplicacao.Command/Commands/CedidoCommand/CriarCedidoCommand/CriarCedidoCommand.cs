using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.CriarCedidoCommand
{
    public class CriarCedidoCommand : IRequest<ApiResponse<int>>
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
}