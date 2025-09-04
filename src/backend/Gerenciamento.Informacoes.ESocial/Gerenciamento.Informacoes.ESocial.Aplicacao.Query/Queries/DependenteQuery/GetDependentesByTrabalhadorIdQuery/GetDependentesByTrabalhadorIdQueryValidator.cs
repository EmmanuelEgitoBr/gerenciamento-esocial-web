using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependentesByTrabalhadorIdQuery;

public class GetDependentesByTrabalhadorIdQueryValidator : AbstractValidator<GetDependentesByTrabalhadorIdQuery>
{
    public GetDependentesByTrabalhadorIdQueryValidator()
    {
        RuleFor(e => e.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
