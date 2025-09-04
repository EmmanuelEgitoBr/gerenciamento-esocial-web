using Gerenciamento.Informacoes.ESocial.Dominio.Entidades.Cadastro;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogStatusCadastroCommand.CriarLogStatusCadastroCommand;

public class CriarLogStatusCadastroCommandHandler : IRequestHandler<CriarLogStatusCadastroCommand, ApiResponse<int>>
{
    private readonly ILogStatusCadastroRepository _logStatusCadastroRepository;

    public CriarLogStatusCadastroCommandHandler(ILogStatusCadastroRepository logStatusCadastroRepository)
    {
        _logStatusCadastroRepository = logStatusCadastroRepository;
    }   

    public async Task<ApiResponse<int>> Handle(CriarLogStatusCadastroCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var logEntity = new LogStatusCadastro
            {
                TrabalhadorId = request.TrabalhadorId,
                EmailTrabalhador = request.EmailTrabalhador,
                IsEmailRecebido = request.IsEmailRecebido,
                StatusCadastro = request.StatusCadastro,
                Pendencias = request.Pendencias,
                DataEventoLog = DateTime.Now
            };

            await _logStatusCadastroRepository.AddAsync(logEntity);

            return new ApiResponse<int>(true, 1, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }
}
