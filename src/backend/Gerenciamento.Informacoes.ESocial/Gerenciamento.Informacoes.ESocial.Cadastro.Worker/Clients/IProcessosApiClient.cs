using Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos.ProcessoLogs;
using Refit;
using Dto = Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Dtos;

namespace Gerenciamento.Informacoes.ESocial.Cadastro.Worker.Clients;

public interface IProcessosApiClient
{
    [Post("/api/v1/processos/log-status-cadastro")]
    Task<Dto.ApiResponse<int>> RegistrarLogStatusAsync([Body] CriarLogStatusCadastroCommand log);

    [Post("/api/v1/processos/log-envio-email")]
    Task<Dto.ApiResponse<int>> RegistrarLogEnvioEmailAsync([Body] CriarLogEnvioEmailCommand log);
}
