using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByIdQuery;

public class GetEstagiarioByIdQueryHandler : IRequestHandler<GetEstagiarioByIdQuery, ApiResponse<EstagiarioDto>>
{
    private readonly IMapper _mapper;
    private readonly IEstagiarioRepository _estagiarioRepository;

    public GetEstagiarioByIdQueryHandler(IMapper mapper, IEstagiarioRepository estagiarioRepository)
    {
        _mapper = mapper;
        _estagiarioRepository = estagiarioRepository;
    }

    public async Task<ApiResponse<EstagiarioDto>> Handle(GetEstagiarioByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _estagiarioRepository.GetByIdAsync(request.EstagiarioId);
            if (entity == null) return new ApiResponse<EstagiarioDto>(false, null, "Estagiário(a) não encontrado(a)");

            var dto = _mapper.Map<EstagiarioDto>(entity);
            return new ApiResponse<EstagiarioDto>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<EstagiarioDto>(false, null, ex.Message);
        }
    }
}
