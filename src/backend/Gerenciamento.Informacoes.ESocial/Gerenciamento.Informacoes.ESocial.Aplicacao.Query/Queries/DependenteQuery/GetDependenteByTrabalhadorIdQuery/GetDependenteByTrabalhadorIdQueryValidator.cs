using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByTrabalhadorIdQuery;

public class GetDependenteByTrabalhadorIdQueryValidator : AbstractValidator<GetDependenteByTrabalhadorIdQuery>
{
    public GetDependenteByTrabalhadorIdQueryValidator()
    {
        RuleFor(e => e.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
