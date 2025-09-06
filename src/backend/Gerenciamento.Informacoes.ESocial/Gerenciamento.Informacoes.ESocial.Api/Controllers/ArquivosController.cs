using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento.Informacoes.ESocial.Api.Controllers;

/// <summary>
/// Controller de gestão de arquivos
/// </summary>
[Route("api/v1/arquivos")]
[ApiController]
public class ArquivosController : ControllerBase
{
    private readonly IArquivoService _arquivoService;

    public ArquivosController(IArquivoService arquivoService)
    {
        _arquivoService = arquivoService;
    }

    /// <summary>
    /// Endpoint responsável pelo download de arquivo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        var result = await _arquivoService.DownloadAsync(id);

        if (!result.Success) return NotFound();

        var arquivo = result.Result;
        return File(arquivo!.Dados, "application/octet-stream", arquivo.NomeArquivo);
    }

    /// <summary>
    /// Endpoint responsável pelo upload de arquivo
    /// </summary>
    /// <param name="file"></param>
    /// <param name="trabalhadorId"></param>
    /// <param name="tipo"></param>
    /// <returns></returns>
    [HttpPost("{trabalhadorId}/upload")]
    public async Task<ActionResult<ApiResponse<string>>> UploadArquivo(IFormFile file, int? trabalhadorId, [FromQuery] string? tipo)
    {
        var result = await _arquivoService.UploadArquivoAsync(file, trabalhadorId, tipo);

        if(!result.Success) return BadRequest(result);

        return Ok(result);
    }
}