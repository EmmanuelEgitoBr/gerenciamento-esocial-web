using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.AtualizarDependenteCommand;

public class AtualizarDependenteCommandValidator : AbstractValidator<AtualizarDependenteCommand>
{
    public AtualizarDependenteCommandValidator()
    {
        RuleFor(d => d.TrabalhadorId).NotNull().WithMessage("O id do trabalhador não pode ser nulo");
    }
}
