using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByTrabalhadorIdQuery;

public class GetCedidoByTrabalhadorIdQueryHandler : IRequestHandler<GetCedidoByTrabalhadorIdQuery, ApiResponse<CedidoDto>>
{
    private readonly ICedidoRepository _cedidoRepository;
    private readonly IMapper _mapper;

    public GetCedidoByTrabalhadorIdQueryHandler(ICedidoRepository cedidoRepository,
        IMapper mapper)
    {
        _cedidoRepository = cedidoRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CedidoDto>> Handle(GetCedidoByTrabalhadorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _cedidoRepository.GetCedidoByTrabalhadorIdAsync(request.TrabalhadorId);

            if(entity == null) return new ApiResponse<CedidoDto>(false, null, "Cedido(a) não encontrado(a)");

            var dto = _mapper.Map<CedidoDto>(entity);

            return new ApiResponse<CedidoDto>(false, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CedidoDto>(false, null, ex.Message);
        }
    }
}
