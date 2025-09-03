using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetAllCedidosQuery;

public class GetAllCedidosQueryHandler : IRequestHandler<GetAllCedidosQuery, ApiResponse<IEnumerable<CedidoDto>>>
{
    private readonly ICedidoRepository _cedidoRepository;
    private readonly IMapper _mapper;

    public GetAllCedidosQueryHandler(IMapper mapper,ICedidoRepository cedidoRepository)
    {
        _mapper = mapper;
        _cedidoRepository = cedidoRepository;
    }

    public async Task<ApiResponse<IEnumerable<CedidoDto>>> Handle(GetAllCedidosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _cedidoRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<CedidoDto>>(entity);

            return new ApiResponse<IEnumerable<CedidoDto>>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<CedidoDto>>(false, null, ex.Message);
        }
    }
}
