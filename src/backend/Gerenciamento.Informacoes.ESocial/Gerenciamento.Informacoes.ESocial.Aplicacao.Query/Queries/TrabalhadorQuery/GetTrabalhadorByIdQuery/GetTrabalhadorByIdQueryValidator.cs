using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByIdQuery;

public class GetTrabalhadorByIdQueryValidator : AbstractValidator<GetTrabalhadorByIdQuery>
{
    public GetTrabalhadorByIdQueryValidator()
    {
        RuleFor(t => t.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
