using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.CedidoQuery.GetCedidoByIdQuery;

public class GetCedidoByIdQueryValidator : AbstractValidator<GetCedidoByIdQuery>
{
    public GetCedidoByIdQueryValidator()
    {
        RuleFor(c => c.CedidoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
