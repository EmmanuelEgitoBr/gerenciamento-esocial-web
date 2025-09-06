using Gerenciamento.Informacoes.ESocial.Aplicacao.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Http;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;

public interface IArquivoService
{
    Task<ApiResponse<ArquivoDto>> DownloadAsync(int arquivoId);
    Task<ApiResponse<string>> UploadArquivoAsync(IFormFile file, int? trabalhadorId, string? tipo);
}
