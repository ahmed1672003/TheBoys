using TheBoys.Application.Features.ContactUs.Commands.SendEmail;
using TheBoys.Application.Features.ContactUs.Service;

namespace TheBoys.Application.Features.ContactUs.Commands;

public sealed class ContactUseComamndsHandler(IContactUsService contactUsService)
    : IRequestHandler<SendEmailCommand, Response>
{
    public Task<Response> Handle(SendEmailCommand command, CancellationToken cancellationToken) =>
        contactUsService.SendEmailAsync(command, cancellationToken);
}
