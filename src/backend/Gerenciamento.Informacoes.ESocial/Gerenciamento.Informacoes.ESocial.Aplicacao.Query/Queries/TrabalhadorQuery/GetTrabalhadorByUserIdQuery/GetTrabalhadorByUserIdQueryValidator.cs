using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.TrabalhadorQuery.GetTrabalhadorByUserIdQuery;

public class GetTrabalhadorByUserIdQueryValidator : AbstractValidator<GetTrabalhadorByUserIdQuery>
{
    public GetTrabalhadorByUserIdQueryValidator()
    {
        RuleFor(t => t.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
