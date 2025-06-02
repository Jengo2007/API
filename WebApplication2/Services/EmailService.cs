using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using WebApplication2.DTO;
using WebApplication2.Interfaces;

namespace WebApplication2.Services;

public class EmailService: IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmail(EmailDto emailDto)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
        email.To.Add(MailboxAddress.Parse(emailDto.To));
        email.Subject = emailDto.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]!), 
            SecureSocketOptions.SslOnConnect);

        await smtp.AuthenticateAsync(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}