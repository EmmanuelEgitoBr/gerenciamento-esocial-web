using Gerenciamento.Informacoes.ESocial.Aplicacao.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;

public interface IEmailService
{
    Task<ApiResponse<string>> SendEmailAsync(EmailModel email);
}
