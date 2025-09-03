using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.EstagiarioCommand.CriarEstagiarioCommand;

public class CriarEstagiarioCommandValidator : AbstractValidator<CriarEstagiarioCommand>
{
    public CriarEstagiarioCommandValidator()
    {
        RuleFor(e => e.TrabalhadorId).NotNull().WithMessage("O id do trabalhador não pode ser nulo");
    }
}
