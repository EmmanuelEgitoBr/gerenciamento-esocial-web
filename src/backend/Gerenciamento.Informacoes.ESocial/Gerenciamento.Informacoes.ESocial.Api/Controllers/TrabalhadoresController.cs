using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.AtualizarTrabalhadorCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.CriarTrabalhadorCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.RemoverTrabalhadorCommand;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetAllTrabalhadorQuery;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    /// <summary>
    /// Controller de gestão de trabalhadores
    /// </summary>
    [Route("api/v1/trabalhadores")]
    [ApiController]
    public class TrabalhadoresController(IMediator mediator,
        ITrabalhadorService trabalhadorService,
        IConfiguration configuration) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ITrabalhadorService _trabalhadorService = trabalhadorService;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Buscar todos os trabalhadors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<TrabalhadorDto>>> GetAllTrabalhadors()
        {
            var result = await _mediator.Send(new GetAllTrabalhadorQuery());

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Buscar trabalhador pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<ApiResponse<TrabalhadorDto>>> GetTrabalhadorById(int id)
        {
            var result = await _mediator.Send(new GetTrabalhadorByIdQuery { TrabalhadorId = id });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Criar um novo trabalhador
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<int>>> CriarTrabalhador([FromBody] CriarTrabalhadorCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Atualizar trabalhador
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<int>>> AtualizarTrabalhador([FromBody] AtualizarTrabalhadorCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Remover um trabalhador
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<int>>> RemoverTrabalhador([FromBody] RemoverTrabalhadorCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("{trabalhadorId}/atualizar-status")]
        public async Task<ActionResult> AtualizarStatusCadastro(int trabalhadorId, 
            [FromQuery] int novoStatus,
            [FromBody] string? pendenciasCadastro)
        {
            string queueName = _configuration["RabbitMq:QueueName"]!;
            var result = await _trabalhadorService.PublicarMensagemMudancaStatus(trabalhadorId, 
                novoStatus, 
                pendenciasCadastro,
                queueName);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}