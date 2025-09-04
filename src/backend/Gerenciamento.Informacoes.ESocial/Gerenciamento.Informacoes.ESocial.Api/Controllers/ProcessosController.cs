using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogStatusCadastroCommand.CriarLogStatusCadastroCommand;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    [Route("api/v1/processos")]
    [ApiController]
    public class ProcessosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Endpoint que salva os logs de mudança de status de cadastro
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("log-status-cadastro")]
        public async Task<ActionResult<ApiResponse<int>>> SalvarLogStatusCadastro([FromBody] CriarLogStatusCadastroCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
