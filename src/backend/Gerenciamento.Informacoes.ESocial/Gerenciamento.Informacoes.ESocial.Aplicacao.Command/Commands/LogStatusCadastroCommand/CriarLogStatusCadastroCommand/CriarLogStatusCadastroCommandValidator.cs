using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogStatusCadastroCommand.CriarLogStatusCadastroCommand;

public class CriarLogStatusCadastroCommandValidator : AbstractValidator<CriarLogStatusCadastroCommand>
{
    public CriarLogStatusCadastroCommandValidator()
    {
        RuleFor(e => e.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
