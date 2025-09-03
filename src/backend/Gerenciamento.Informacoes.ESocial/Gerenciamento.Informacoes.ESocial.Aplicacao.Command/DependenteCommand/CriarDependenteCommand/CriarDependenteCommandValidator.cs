using FluentValidation;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.DependenteCommand.CriarDependenteCommand
{
    public class CriarDependenteCommandValidator : AbstractValidator<CriarDependenteCommand>
    {
        public CriarDependenteCommandValidator()
        {
            RuleFor(d => d.TrabalhadorId).NotNull().WithMessage("O id do trabalhador não pode ser nulo");
        }
    }
}
