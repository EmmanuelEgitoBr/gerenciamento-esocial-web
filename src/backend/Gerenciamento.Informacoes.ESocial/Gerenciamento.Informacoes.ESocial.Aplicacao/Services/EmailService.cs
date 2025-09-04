using Gerenciamento.Informacoes.ESocial.Aplicacao.Models;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Services.Interfaces;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Settings;
using Gerenciamento.Informacoes.ESocial.Dominio.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<ApiResponse<string>> SendEmailAsync(EmailModel email)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Censo Esocial", _settings.From));
            message.To.Add(new MailboxAddress("", email.To));
            message.Subject = email.Subject;
            message.Body = new TextPart("plain")
            {
                Text = email.Content
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return new ApiResponse<string>(true, email.To, null);
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>(false, null, ex.Message);
        }
    }
}
