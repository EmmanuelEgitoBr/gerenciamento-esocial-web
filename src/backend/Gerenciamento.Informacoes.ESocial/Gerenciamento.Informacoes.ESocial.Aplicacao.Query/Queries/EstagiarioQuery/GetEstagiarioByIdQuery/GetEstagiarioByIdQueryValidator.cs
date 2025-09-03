using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByIdQuery;

public class GetEstagiarioByIdQueryValidator : AbstractValidator<GetEstagiarioByIdQuery>
{
    public GetEstagiarioByIdQueryValidator()
    {
        RuleFor(e => e.EstagiarioId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
