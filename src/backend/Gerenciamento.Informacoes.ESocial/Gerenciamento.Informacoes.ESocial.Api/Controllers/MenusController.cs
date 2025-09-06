using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    [Route("api/v1/menus")]
    [ApiController]
    public class MenusController : ControllerBase
    {


        [HttpGet("carregar-menu/{id}")]
        public async Task<ActionResult> CarregarMenuParaTrabalhador(int trabalhadorId)
        {
            return Ok();
        }
    }
}
