using Gerenciamento.Informacoes.ESocial.Aplicacao.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Resources.Constants;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Utils;
using Gerenciamento.Informacoes.ESocial.Dominio.Enums;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces;
using Gerenciamento.Informacoes.ESocial.Dominio.Interfaces.Messaging;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services;

public class TrabalhadorService : ITrabalhadorService
{
    private readonly ITrabalhadorRepository _trabalhadorRepository;
    private readonly IRabbitMqMessageSenderService _rabbitMqService;

    public TrabalhadorService(ITrabalhadorRepository trabalhadorRepository,
        IRabbitMqMessageSenderService rabbitMqService)
    {
        _trabalhadorRepository = trabalhadorRepository;
        _rabbitMqService = rabbitMqService;
    }

    public async Task<ApiResponse<MessageModel>> PublicarMensagemMudancaStatus(int trabalhadorId, 
        int novoStatus, 
        string? pendenciasCadastro,
        string queueName)
    {
        try
        {
            var trabalhador = await _trabalhadorRepository.GetByIdAsync(trabalhadorId);

            if (trabalhador == null) return new ApiResponse<MessageModel>(false, null, "Trabalhador(a) não encontrado");

            var emailTrabalhador = trabalhador.Contato!.Email1;
            var emailModel = MontarEmailModel(trabalhador.Nome!, emailTrabalhador!, novoStatus, pendenciasCadastro);

            var message = new MessageModel
            {
                TrabalhadorId = trabalhadorId,
                EmailTrabalhador = emailTrabalhador,
                StatusCadastro = (StatusCadastro)novoStatus,
                Pendencias = pendenciasCadastro,
                Email = emailModel
            };

            await _rabbitMqService.SendMessageAsync(message, queueName);

            trabalhador.StatusCadastro = (StatusCadastro)novoStatus;
            await _trabalhadorRepository.UpdateAsync(trabalhador);

            return new ApiResponse<MessageModel>(true, message, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<MessageModel>(false, null, ex.Message);
        }
    }

    private EmailModel MontarEmailModel(string nomeTrabalhador, 
        string emailTrabalhador, 
        int novoStatus, 
        string? pendenciasCadastro)
    {
        string pendencias = pendenciasCadastro!;
        if (string.IsNullOrEmpty(pendenciasCadastro)) pendencias = "";

        string situacaoCadastro;

        switch ((StatusCadastro)novoStatus)
        {
            case StatusCadastro.Criado:
                situacaoCadastro = SituacaoCadastro.Criado;
                break;
            case StatusCadastro.Pendente:
                situacaoCadastro = SituacaoCadastro.Pendente;
                break;
            case StatusCadastro.Concluido:
                situacaoCadastro = SituacaoCadastro.Concluido;
                break;
            default:
                situacaoCadastro = string.Empty;
                break;
        }

        string assuntoEmail;

        switch ((StatusCadastro)novoStatus)
        {
            case StatusCadastro.Criado:
                assuntoEmail = AssuntoEmail.Criado;
                break;
            case StatusCadastro.Pendente:
                assuntoEmail = AssuntoEmail.Pendente;
                break;
            case StatusCadastro.Concluido:
                assuntoEmail = AssuntoEmail.Concluido;
                break;
            default:
                assuntoEmail = string.Empty;
                break;
        }

        string body = EmailBuilder.ConstruirCorpoEmail(nomeTrabalhador, situacaoCadastro, pendencias);

        return new EmailModel
        {
            From = string.Empty,
            To = emailTrabalhador,
            Subject = assuntoEmail,
            Content = body
        };
    }
}
