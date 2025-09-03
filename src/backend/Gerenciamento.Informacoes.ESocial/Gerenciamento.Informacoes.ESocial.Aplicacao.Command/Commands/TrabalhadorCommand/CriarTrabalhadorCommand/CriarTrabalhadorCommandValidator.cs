using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.TrabalhadorCommand.CriarTrabalhadorCommand;

public class CriarTrabalhadorCommandValidator : AbstractValidator<CriarTrabalhadorCommand>
{
    public CriarTrabalhadorCommandValidator()
    {
        RuleFor(t => t.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage("O nome do trabalhador não pode ser nulo ou vazio");
    }
}
