using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByTrabalhadorIdQuery;

public class GetEstagiarioByTrabalhadorIdQueryHandler : IRequestHandler<GetEstagiarioByTrabalhadorIdQuery, ApiResponse<IEnumerable<EstagiarioDto>>>
{
    private readonly IEstagiarioRepository _estagiarioRepository;
    private readonly IMapper _mapper;

    public GetEstagiarioByTrabalhadorIdQueryHandler(IEstagiarioRepository estagiarioRepository,
        IMapper mapper)
    {
        _estagiarioRepository = estagiarioRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<EstagiarioDto>>> Handle(GetEstagiarioByTrabalhadorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _estagiarioRepository.GetEstagiariosByTrabalhadorIdAsync(request.TrabalhadorId);

            if (entity == null) return new ApiResponse<IEnumerable<EstagiarioDto>>(false, null, "Estagiario(a) não encontrado(a)");

            var dto = _mapper.Map<IEnumerable<EstagiarioDto>>(entity);

            return new ApiResponse<IEnumerable<EstagiarioDto>>(false, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<EstagiarioDto>>(false, null, ex.Message);
        }
    }
}
