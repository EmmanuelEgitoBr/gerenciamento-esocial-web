using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers
{
    /// <summary>
    /// Controller de gestão de arquivos
    /// </summary>
    [Route("api/v1/arquivos")]
    [ApiController]
    public class ArquivosController : ControllerBase
    {

    /// <summary>
    /// Salvar um arquivo na base de dados
    /// </summary>
    /// <returns></returns>
    [HttpPost("save")]
        public IActionResult SalvarArquivo(IFormFile file)
        {
            return Ok(file.FileName);
        }
    }
}