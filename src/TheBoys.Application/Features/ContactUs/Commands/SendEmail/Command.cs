using TheBoys.Shared.Enums;

namespace TheBoys.Application.Features.ContactUs.Commands.SendEmail;

public sealed record SendEmailCommand(
    string Email,
    string Subject,
    string Body,
    int RatingValue,
    MailType Type
) : IRequest<Response>;
