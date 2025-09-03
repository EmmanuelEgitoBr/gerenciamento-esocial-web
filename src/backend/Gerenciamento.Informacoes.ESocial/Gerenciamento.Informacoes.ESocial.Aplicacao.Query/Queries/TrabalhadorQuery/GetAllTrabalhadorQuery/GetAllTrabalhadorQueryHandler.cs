using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetAllTrabalhadorQuery;

public class GetAllTrabalhadorQueryHandler : IRequestHandler<GetAllTrabalhadorQuery, ApiResponse<IEnumerable<TrabalhadorDto>>>
{
    private readonly IMapper _mapper;
    private readonly ITrabalhadorRepository _trabalhadorRepository;

    public GetAllTrabalhadorQueryHandler(IMapper mapper, ITrabalhadorRepository trabalhadorRepository)
    {
        _mapper = mapper;
        _trabalhadorRepository = trabalhadorRepository;
    }

    public async Task<ApiResponse<IEnumerable<TrabalhadorDto>>> Handle(GetAllTrabalhadorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _trabalhadorRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<TrabalhadorDto>>(entity);

            return new ApiResponse<IEnumerable<TrabalhadorDto>>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<TrabalhadorDto>>(false, null, ex.Message);
        }
    }
}
