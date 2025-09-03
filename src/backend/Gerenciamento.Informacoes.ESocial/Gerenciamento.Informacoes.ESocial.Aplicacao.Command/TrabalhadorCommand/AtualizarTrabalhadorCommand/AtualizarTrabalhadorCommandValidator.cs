using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.AtualizarTrabalhadorCommand;

public class AtualizarTrabalhadorCommandValidator : AbstractValidator<AtualizarTrabalhadorCommand>
{
    public AtualizarTrabalhadorCommandValidator()
    {
        RuleFor(t => t.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage("O nome do trabalhador não pode ser nulo ou vazio");
    }
}
