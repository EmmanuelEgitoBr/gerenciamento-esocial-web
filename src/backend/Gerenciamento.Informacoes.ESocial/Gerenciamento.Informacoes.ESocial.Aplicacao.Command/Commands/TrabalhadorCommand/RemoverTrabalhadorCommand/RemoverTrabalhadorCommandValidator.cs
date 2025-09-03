using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.RemoverTrabalhadorCommand;

public class RemoverTrabalhadorCommandValidator : AbstractValidator<RemoverTrabalhadorCommand>
{
    public RemoverTrabalhadorCommandValidator()
    {
        RuleFor(c => c.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
