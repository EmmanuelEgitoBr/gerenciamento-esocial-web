using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.AtualizarDependenteCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.CriarDependenteCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.RemoverDependenteCommand;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetAllDependenteQuery;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByTrabalhadorIdQuery;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    /// <summary>
    /// Controller de gestão de dependentes
    /// </summary>
    [Route("api/v1/dependentes")]
    [ApiController]
    public class DependentesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

    /// <summary>
        /// Buscar todos os dependentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<DependenteDto>>> GetAllDependentes()
        {
            var result = await _mediator.Send(new GetAllDependenteQuery());

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Buscar dependente pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse<DependenteDto>>> GetDependenteById(int id)
        {
            var result = await _mediator.Send(new GetDependenteByIdQuery { DependenteId = id });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Retorna os dependentes por id do trabalhador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("trabalhador/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<DependenteDto>>>> GetDependentesByTrabalhadorId(int id)
        {
            var result = await _mediator.Send(new GetDependenteByTrabalhadorIdQuery { TrabalhadorId = id });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Criar um novo dependente
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<int>>> CriarDependente([FromBody] CriarDependenteCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Atualizar dependente
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<int>>> AtualizarDependente([FromBody] AtualizarDependenteCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Remover um dependente
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<int>>> RemoverDependente([FromBody] RemoverDependenteCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}