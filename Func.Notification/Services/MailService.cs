using Func.Notification.Configuration;
using Func.Notification.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace Func.Notification.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailService(SmtpSettings smtpSettings, ILogger logger)
        {
            _smtpSettings = smtpSettings
                ?? throw new ArgumentNullException(nameof(smtpSettings));
        }

        public async Task<string> SendMail(MailEventModel model, string acao)
        {
            var msgModel = acao switch
            {
                "registered-mail" => new
                {
                    Subject = "USUÁRIO REGISTRADO | CONFIRMAR EMAIL",
                    Body = @$"<h1>Bem vindo {model.FullName}</h1>
                              </br>
                              <h2>Hora de confirmar o email!</h2>
                              <p>Para confirmar o email utilize o token de confirmação abaixo:</p>
                              </br>
                              <p><strong>Token:</strong>{model.Token}</p>"
                },
                "resetPass-mail" => new
                {
                    Subject = "REDEFINIR SENHA DO USUÁRIO",
                    Body = @$"<h1>Olá</h1>
                              </br>
                              <h2>Vamos recuperar sua senha.</h2>
                              <p>Utilize o token abaixo para confirmar a alteração de sua senha:</p>
                              </br>
                              <p><strong>Token:</strong>{model.Token}</p>"
                },
                _ => throw new ArgumentException()
            };

            try
            {
                var msg = new MimeMessage();

                msg.From.Add(MailboxAddress.Parse(_smtpSettings.SmtpUsername));
                msg.To.Add(MailboxAddress.Parse(model.Email));
                msg.Subject = msgModel.Subject;
                msg.Body = new TextPart(TextFormat.Html)
                {
                    Text = msgModel.Body
                };

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

                client.Connect(_smtpSettings.SmtpAddress,
                    _smtpSettings.SmtpPort, _smtpSettings.UseSsl);

                client.Authenticate(_smtpSettings.SmtpUsername,
                    _smtpSettings.SmtpPassword);

                await client.SendAsync(msg);
                await client.DisconnectAsync(true);

                return $"Email enviado para {msg.To} em {DateTime.Now}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
