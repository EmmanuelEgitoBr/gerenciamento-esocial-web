using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogEnvioEmailCommand.CriarLogEnvioEmailCommand;

public class CriarLogEnvioEmailCommandHandler : IRequestHandler<CriarLogEnvioEmailCommand, ApiResponse<int>>
{
    private readonly ILogEnvioEmailRepository _logEnvioEmailRepository;

    public CriarLogEnvioEmailCommandHandler(ILogEnvioEmailRepository logEnvioEmailRepository)
    {
        _logEnvioEmailRepository = logEnvioEmailRepository;
    }

    public async Task<ApiResponse<int>> Handle(CriarLogEnvioEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var log = new LogEnvioEmail
            {
                TrabalhadorId = request.TrabalhadorId,
                EmailTrabalhador = request.EmailTrabalhador,
                DescricaoLog = request.DescricaoLog,
                DataEventoLog = DateTime.Now
            };

            await _logEnvioEmailRepository.AddAsync(log);

            return new ApiResponse<int>(true, 1, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }
}
