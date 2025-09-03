using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.AtualizarEstagiarioCommand;

public class AtualizarEstagiarioCommandValidator : AbstractValidator<AtualizarEstagiarioCommand>
{
    public AtualizarEstagiarioCommandValidator()
    {
        RuleFor(e => e.TrabalhadorId).NotNull().WithMessage("O id do trabalhador não pode ser nulo");
    }
}
