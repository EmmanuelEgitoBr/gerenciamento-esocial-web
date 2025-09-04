using Gerenciamento.Informacoes.ESocial.Aplicacao.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    [Route("api/v1/emails")]
    [ApiController]
    public class EmailsController(IEmailService emailService) : ControllerBase
    {
        private readonly IEmailService _emailService = emailService;

        /// <summary>
        /// Endpoint para envio de email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("send-email")]
        public async Task<ActionResult> EnviarEmailConfirmacao([FromBody] EmailModel model)
        {
            var result = await _emailService.SendEmailAsync(model);

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
    }
}
