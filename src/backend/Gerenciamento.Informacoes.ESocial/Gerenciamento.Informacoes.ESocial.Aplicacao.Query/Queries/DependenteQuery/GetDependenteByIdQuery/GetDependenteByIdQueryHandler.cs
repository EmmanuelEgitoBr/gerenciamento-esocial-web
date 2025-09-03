using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByIdQuery;

public class GetDependenteByIdQueryHandler : IRequestHandler<GetDependenteByIdQuery, ApiResponse<DependenteDto>>
{
    private readonly IMapper _mapper;
    private readonly IDependenteRepository _dependenteRepository;

    public GetDependenteByIdQueryHandler(IMapper mapper, IDependenteRepository dependenteRepository)
    {
        _mapper = mapper;
        _dependenteRepository = dependenteRepository;
    }

    public async Task<ApiResponse<DependenteDto>> Handle(GetDependenteByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dependenteRepository.GetByIdAsync(request.DependenteId);
            if (entity == null) return new ApiResponse<DependenteDto>(false, null, "Dependente não encontrado(a)");

            var dto = _mapper.Map<DependenteDto>(entity);
            return new ApiResponse<DependenteDto>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<DependenteDto>(false, null, ex.Message);
        }
    }
}
