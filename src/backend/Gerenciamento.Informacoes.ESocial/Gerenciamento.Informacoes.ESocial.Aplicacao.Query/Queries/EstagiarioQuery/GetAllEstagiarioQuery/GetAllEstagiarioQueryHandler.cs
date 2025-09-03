using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetAllEstagiarioQuery;

public class GetAllEstagiarioQueryHandler : IRequestHandler<GetAllEstagiarioQuery, ApiResponse<IEnumerable<EstagiarioDto>>>
{
    private readonly IMapper _mapper;
    private readonly IEstagiarioRepository _estagiarioRepository;

    public GetAllEstagiarioQueryHandler(IMapper mapper, IEstagiarioRepository estagiarioRepository)
    {
        _mapper = mapper;
        _estagiarioRepository = estagiarioRepository;
    }

    public async Task<ApiResponse<IEnumerable<EstagiarioDto>>> Handle(GetAllEstagiarioQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _estagiarioRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<EstagiarioDto>>(entity);

            return new ApiResponse<IEnumerable<EstagiarioDto>>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<EstagiarioDto>>(false, null, ex.Message);
        }
    }
}
