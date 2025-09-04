using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    [Route("api/v1/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult EnviarEmailConfirmacao()
        {
            return Ok();
        }
    }
}
