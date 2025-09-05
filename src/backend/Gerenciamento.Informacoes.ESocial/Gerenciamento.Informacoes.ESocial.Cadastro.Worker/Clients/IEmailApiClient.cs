using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.Messaging;
using Refit;
using Dto = Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Clients;

public interface IEmailApiClient
{
    [Post("/api/v1/emails/send-email")]
    Task<Dto.ApiResponse<string>> SendEmailAsync([Body] EmailModel email);
}
