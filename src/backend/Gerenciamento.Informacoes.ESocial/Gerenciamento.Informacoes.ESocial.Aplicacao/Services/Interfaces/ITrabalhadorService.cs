using Gerenciamento.Informacoes.ESocial.Aplicacao.Models;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;

public interface ITrabalhadorService
{
    Task<ApiResponse<MessageModel>> PublicarMensagemMudancaStatus(int trabalhadorId,
        int novoStatus,
        string? pendenciasCadastro,
        string queueName);
}
