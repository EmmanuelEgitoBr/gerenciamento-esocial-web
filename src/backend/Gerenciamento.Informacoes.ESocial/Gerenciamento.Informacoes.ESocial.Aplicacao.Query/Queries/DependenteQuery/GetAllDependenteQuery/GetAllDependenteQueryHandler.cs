using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetAllDependenteQuery;

public class GetAllDependenteQueryHandler : IRequestHandler<GetAllDependenteQuery, ApiResponse<IEnumerable<DependenteDto>>>
{
    private readonly IDependenteRepository _dependenteRepository;
    private readonly IMapper _mapper;

    public GetAllDependenteQueryHandler(IDependenteRepository dependenteRepository,
        IMapper mapper)
    {
        _dependenteRepository = dependenteRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<DependenteDto>>> Handle(GetAllDependenteQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dependenteRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<DependenteDto>>(entity);

            return new ApiResponse<IEnumerable<DependenteDto>>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<DependenteDto>>(false, null, ex.Message);
        }
    }
}
