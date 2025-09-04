using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.AtualizarEstagiarioCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.CriarEstagiarioCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.RemoverEstagiarioCommand;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetAllEstagiarioQuery;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByTrabalhadorIdQuery;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    /// <summary>
    /// Controller de gestão de estagiários
    /// </summary>
    [Route("api/v1/estagiarios")]
    [ApiController]
    public class EstagiariosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Buscar todos os estagiarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<EstagiarioDto>>> GetAllEstagiarios()
        {
            var result = await _mediator.Send(new GetAllEstagiarioQuery());

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Buscar estagiario pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse<EstagiarioDto>>> GetEstagiarioById(int id)
        {
            var result = await _mediator.Send(new GetEstagiarioByIdQuery { EstagiarioId = id });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Retorna estagiário(a) pelo id do trabalhador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("trabalhador/{id}")]
        public async Task<ActionResult<ApiResponse<EstagiarioDto>>> GetEstagiariosBrTrabalhadorId(int id)
        {
            var result = await _mediator.Send(new GetEstagiarioByTrabalhadorIdQuery { TrabalhadorId = id });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Criar um novo estagiario
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<int>>> CriarEstagiario([FromBody] CriarEstagiarioCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Atualizar estagiario
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<int>>> AtualizarEstagiario([FromBody] AtualizarEstagiarioCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Remover um estagiario
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<int>>> RemoverEstagiario([FromBody] RemoverEstagiarioCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}