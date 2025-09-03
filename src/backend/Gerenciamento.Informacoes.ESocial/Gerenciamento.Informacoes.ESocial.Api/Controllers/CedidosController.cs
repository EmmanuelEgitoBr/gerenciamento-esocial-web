using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.AtualizarCedidoCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.CriarCedidoCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.RemoverCedidoCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetAllCedidosQuery;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    /// <summary>
    /// Controller de gestão de cedidos
    /// </summary>
    [Route("api/v1/cedidos")]
    [ApiController]
    public class CedidosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Buscar todos os cedidos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<CedidoDto>>> GetAllCedidos()
        {
            var result = await _mediator.Send(new GetAllCedidosQuery());

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Buscar cedido pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse<CedidoDto>>> GetCedidoById(int id)
        {
            var result = await _mediator.Send(new GetCedidoByIdQuery { CedidoId = id });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Criar um novo cedido
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<int>>> CriarCedido([FromBody] CriarCedidoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Atualizar cedido
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<int>>> AtualizarCedido([FromBody] AtualizarCedidoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Remover um cedido
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<int>>> RemoverCedido([FromBody] RemoverCedidoCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
