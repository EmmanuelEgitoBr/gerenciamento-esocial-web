using Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Infra.Sql.Repositorios;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.RemoverTrabalhadorCommand;

public class RemoverTrabalhadorCommandHandler : IRequestHandler<RemoverTrabalhadorCommand, ApiResponse<int>>
{
    private readonly ITrabalhadorRepository _trabalhadorRepository;

    public RemoverTrabalhadorCommandHandler(ITrabalhadorRepository trabalhadorRepository)
    {
        _trabalhadorRepository = trabalhadorRepository;
    }

    public async Task<ApiResponse<int>> Handle(RemoverTrabalhadorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var trabalhador = await _trabalhadorRepository.GetByIdAsync(request.TrabalhadorId);

            if (trabalhador == null) return new ApiResponse<int>(false, 0, "Trabalhador(a) não encontrado(a)");

            // Criar método para buscar dependentes pelo id do trabalhador e depois removê-los do banco
            // Criar método para buscar cedidos pelo id do trabalhador e depois removê-los do banco
            // Criar método para buscar estagiários pelo id do trabalhador e depois removê-los do banco

            await _trabalhadorRepository.RemoveAsync(trabalhador);

            return new ApiResponse<int>(true, trabalhador.TrabalhadorId, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<int>(false, 0, ex.Message);
        }
    }
}
