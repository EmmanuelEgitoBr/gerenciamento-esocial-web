using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependentesByTrabalhadorIdQuery;

public class GetDependentesByTrabalhadorIdQueryHandler : IRequestHandler<GetDependentesByTrabalhadorIdQuery, ApiResponse<IEnumerable<DependenteDto>>>
{
    private readonly IDependenteRepository _dependenteRepository;
    private readonly IMapper _mapper;

    public GetDependentesByTrabalhadorIdQueryHandler(IDependenteRepository dependenteRepository,
        IMapper mapper)
    {
        _dependenteRepository = dependenteRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<DependenteDto>>> Handle(GetDependentesByTrabalhadorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dependenteRepository.GetDependentesByTrabalhadorIdAsync(request.TrabalhadorId);

            if (entity == null) return new ApiResponse<IEnumerable<DependenteDto>>(false, null, "Dependente não encontrado");

            var dto = _mapper.Map<IEnumerable<DependenteDto>>(entity);

            return new ApiResponse<IEnumerable<DependenteDto>>(false, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<DependenteDto>>(false, null, ex.Message);
        }
    }
}
