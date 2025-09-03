using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Queries.EstagiarioQuery.GetEstagiarioByTrabalhadorIdQuery;

public class GetEstagiarioByTrabalhadorIdQueryValidator : AbstractValidator<GetEstagiarioByTrabalhadorIdQuery>
{
    public GetEstagiarioByTrabalhadorIdQueryValidator()
    {
        RuleFor(e => e.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
