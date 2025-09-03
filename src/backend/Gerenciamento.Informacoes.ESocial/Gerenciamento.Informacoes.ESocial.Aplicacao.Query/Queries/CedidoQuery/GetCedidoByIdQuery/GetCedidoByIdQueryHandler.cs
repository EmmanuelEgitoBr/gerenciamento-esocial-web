using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByIdQuery;

public class GetCedidoByIdQueryHandler : IRequestHandler<GetCedidoByIdQuery, ApiResponse<CedidoDto>>
{
    private readonly IMapper _mapper;
    private readonly ICedidoRepository _cedidoRepository;

    public GetCedidoByIdQueryHandler(IMapper mapper, ICedidoRepository cedidoRepository)
    {
        _mapper = mapper;
        _cedidoRepository = cedidoRepository;
    }

    public async Task<ApiResponse<CedidoDto>> Handle(GetCedidoByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _cedidoRepository.GetByIdAsync(request.CedidoId);
            if (entity == null) return new ApiResponse<CedidoDto>(false, null, "Cedido(a) não encontrado(a)");

            var dto = _mapper.Map<CedidoDto>(entity);
            return new ApiResponse<CedidoDto>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CedidoDto>(false, null, ex.Message);
        }
    }
}
