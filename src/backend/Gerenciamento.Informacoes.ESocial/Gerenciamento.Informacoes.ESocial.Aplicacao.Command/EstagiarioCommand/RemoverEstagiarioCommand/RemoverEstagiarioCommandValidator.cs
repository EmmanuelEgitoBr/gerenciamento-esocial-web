using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.RemoverEstagiarioCommand;

public class RemoverEstagiarioCommandValidator : AbstractValidator<RemoverEstagiarioCommand>
{
    public RemoverEstagiarioCommandValidator()
    {
        RuleFor(c => c.EstagiarioId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
