using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.CedidoCommand.CriarCedidoCommand;

public class CriarCedidoCommandValidator : AbstractValidator<CriarCedidoCommand>
{
    public CriarCedidoCommandValidator()
    {
        RuleFor(c => c.TrabalhadorId).NotNull().WithMessage("O id do trabalhador não pode ser nulo");
    }
}
