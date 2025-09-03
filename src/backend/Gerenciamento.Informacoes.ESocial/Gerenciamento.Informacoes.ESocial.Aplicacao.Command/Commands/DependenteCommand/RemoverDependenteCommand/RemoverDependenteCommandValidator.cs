using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.RemoverDependenteCommand;

public class RemoverDependenteCommandValidator : AbstractValidator<RemoverDependenteCommand>
{
    public RemoverDependenteCommandValidator()
    {
        RuleFor(c => c.DependenteId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
