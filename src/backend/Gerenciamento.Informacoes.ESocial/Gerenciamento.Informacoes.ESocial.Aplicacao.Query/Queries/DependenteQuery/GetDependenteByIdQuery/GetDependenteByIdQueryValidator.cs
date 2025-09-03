using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.DependenteQuery.GetDependenteByIdQuery;

public class GetDependenteByIdQueryValidator : AbstractValidator<GetDependenteByIdQuery>
{
    public GetDependenteByIdQueryValidator()
    {
        RuleFor(d => d.DependenteId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
