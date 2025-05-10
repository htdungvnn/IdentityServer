using IdentityServer.WebApp.Areas.Identity.Data.IdentityServer;
using IdentityServer.WebApp.MailService.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IdentityServer.WebApp.MailService.Service;

public class EmailSender :  IEmailSender
{
    private readonly MailSettings _mailSettings;

    public EmailSender(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.FromEmail));
        emailMessage.To.Add(new MailboxAddress(email, email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = message };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, false);
            await client.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}