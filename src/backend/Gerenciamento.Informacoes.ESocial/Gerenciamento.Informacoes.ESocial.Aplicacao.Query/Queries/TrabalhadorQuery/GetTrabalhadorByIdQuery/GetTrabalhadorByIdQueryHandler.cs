using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using MediatR;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByIdQuery;

public class GetTrabalhadorByIdQueryHandler : IRequestHandler<GetTrabalhadorByIdQuery, ApiResponse<TrabalhadorDto>>
{
    private readonly IMapper _mapper;
    private readonly ITrabalhadorRepository _trabalhadorRepository;

    public GetTrabalhadorByIdQueryHandler(IMapper mapper, ITrabalhadorRepository trabalhadorRepository)
    {
        _mapper = mapper;
        _trabalhadorRepository = trabalhadorRepository;
    }

    public async Task<ApiResponse<TrabalhadorDto>> Handle(GetTrabalhadorByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _trabalhadorRepository.GetByIdAsync(request.TrabalhadorId);
            if (entity == null) return new ApiResponse<TrabalhadorDto>(false, null, "Trabalhador(a) não encontrado(a)");

            var dto = _mapper.Map<TrabalhadorDto>(entity);
            return new ApiResponse<TrabalhadorDto>(true, dto, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<TrabalhadorDto>(false, null, ex.Message);
        }
    }
}
