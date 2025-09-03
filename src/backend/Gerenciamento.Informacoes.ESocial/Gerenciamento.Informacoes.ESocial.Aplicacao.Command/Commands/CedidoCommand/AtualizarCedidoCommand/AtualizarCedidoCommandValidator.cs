using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.AtualizarCedidoCommand;

public class AtualizarCedidoCommandValidator : AbstractValidator<AtualizarCedidoCommand>
{
    public AtualizarCedidoCommandValidator()
    {
        RuleFor(c => c.TrabalhadorId).NotNull().WithMessage("O id do trabalhador não pode ser nulo");
    }
}
