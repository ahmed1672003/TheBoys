using TheBoys.Application.Features.ContactUs.Commands.SendEmail;

namespace TheBoys.Application.Features.ContactUs.Service;

public interface IContactUsService
{
    Task<Response> SendEmailAsync(
        SendEmailCommand command,
        CancellationToken cancellationToken = default
    );
}
