using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Microsoft.AspNetCore.Http;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services;

public class ArquivoService : IArquivoService
{
    private readonly IArquivoRepository _arquivoRepository;
    private readonly IMapper _mapper;

    public ArquivoService(IArquivoRepository arquivoRepository,
        IMapper mapper)
    {
        _arquivoRepository = arquivoRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ArquivoDto>> DownloadAsync(int arquivoId)
    {
        try
        {
            var arquivo = await _arquivoRepository.GetByIdAsync(arquivoId);
            var arquivoDto = _mapper.Map<ArquivoDto>(arquivo);

            if (arquivo == null) return new ApiResponse<ArquivoDto>
            {
                Success = false,
                Result = null,
                ErrorMessage = ""
            };

            return new ApiResponse<ArquivoDto>
            {
                Success = true,
                Result = arquivoDto
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<ArquivoDto>
            {
                Success = false,
                Result = null,
                ErrorMessage = ex.Message
            };
        } 
    }

    public async Task<ApiResponse<string>> UploadArquivoAsync(IFormFile file,int? trabalhadorId,string? tipo)
    {
        try
        {
            if (file == null || file.Length == 0)
                return new ApiResponse<string>
                {
                    Success = false,
                    Result = string.Empty,
                    ErrorMessage = "Nenhum arquivo enviado."
                };

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var arquivo = new Arquivo
            {
                TrabalhadorId = trabalhadorId,
                Tipo = tipo,
                NomeArquivo = file.FileName,
                Tamanho = file.Length,
                Dados = memoryStream.ToArray(),
                DataUpload = DateTime.UtcNow
            };

            await _arquivoRepository.AddAsync(arquivo);

            return new ApiResponse<string>
            {
                Success = true,
                Result = arquivo.NomeArquivo
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>
            {
                Success = false,
                Result = string.Empty,
                ErrorMessage = ex.Message
            };
        }
    }
}
