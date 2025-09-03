using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByTrabalhadorIdQuery;

public class GetCedidoByTrabalhadorIdQueryValidator : AbstractValidator<GetCedidoByTrabalhadorIdQuery>
{
    public GetCedidoByTrabalhadorIdQueryValidator()
    {
        RuleFor(e => e.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
