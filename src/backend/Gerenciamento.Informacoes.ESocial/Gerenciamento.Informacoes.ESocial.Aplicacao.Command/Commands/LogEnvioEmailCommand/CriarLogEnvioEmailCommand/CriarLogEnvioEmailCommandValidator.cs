using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Command.Commands.LogEnvioEmailCommand.CriarLogEnvioEmailCommand;

public class CriarLogEnvioEmailCommandValidator : AbstractValidator<CriarLogEnvioEmailCommand>
{
    public CriarLogEnvioEmailCommandValidator()
    {
        RuleFor(e => e.TrabalhadorId)
            .NotNull()
            .NotEmpty()
            .WithMessage("O id não pode ser nulo");
    }
}
