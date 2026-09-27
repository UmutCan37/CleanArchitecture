
using CleanArchitecture.Application.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanArchitecture.Infrastructure.Email;

public sealed class MailService : IMailService
{
    private readonly EmailOptions _options;

    public MailService(IOptions<EmailOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_options.From));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _options.Host,
            _options.Port,
            _options.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(_options.UserName))
            await client.AuthenticateAsync(_options.UserName, _options.Password, cancellationToken);

        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}