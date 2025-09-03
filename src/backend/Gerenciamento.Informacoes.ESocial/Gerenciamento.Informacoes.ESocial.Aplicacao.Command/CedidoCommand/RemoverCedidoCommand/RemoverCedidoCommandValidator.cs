using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.RemoverCedidoCommand;

public class RemoverCedidoCommandValidator : AbstractValidator<RemoverCedidoCommand>
{
    public RemoverCedidoCommandValidator()
    {
        RuleFor(c => c.CedidoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
