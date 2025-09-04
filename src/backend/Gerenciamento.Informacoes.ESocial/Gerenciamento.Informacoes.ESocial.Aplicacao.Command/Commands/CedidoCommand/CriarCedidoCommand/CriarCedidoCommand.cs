using Gerenciamento.Informacoes.ESocial.Dominio.Models;
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
        public int OnusCessReq { get; set; }
        public int? Categoria { get; set; }
    }
}