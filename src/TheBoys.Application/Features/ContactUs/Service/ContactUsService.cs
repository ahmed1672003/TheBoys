using System.Text;
using Microsoft.Extensions.Options;
using TheBoys.Application.Features.ContactUs.Commands.SendEmail;
using TheBoys.Application.Settings;
using TheBoys.Shared.Enums;
using TheBoys.Shared.Externals.Email;

namespace TheBoys.Application.Features.ContactUs.Service;

public sealed class ContactUsService(
    IEmailService emailService,
    IOptions<EmailSettings> emailOptions
) : IContactUsService
{
    EmailSettings _emailSettings => emailOptions.Value;

    public async Task<Response> SendEmailAsync(
        SendEmailCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var body = new StringBuilder(command.Body);

        if (command.Type == MailType.Rating)
        {
            body.AppendLine($"Rating: {command.RatingValue}");
        }

        var mailContract = MailAddressContract.Create(
            _emailSettings.MailHost,
            _emailSettings.MailUsername,
            _emailSettings.MailPasswword,
            _emailSettings.MailPort,
            _emailSettings.MailSender,
            command.Email,
            new List<string>() { "ahmedadel1672003@gmail.com" },
            command.Subject,
            body.ToString(),
            false
        );

        await emailService.SendEmailAsync(mailContract, cancellationToken);
        return new Response() { Success = true };
    }
}
