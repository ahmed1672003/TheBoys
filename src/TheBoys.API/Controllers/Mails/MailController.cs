using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TheBoys.API.Bases.Responses;
using TheBoys.API.Controllers.Mails.Requests;
using TheBoys.API.Settings;
using TheBoys.Contracts.Email;
using TheBoys.Shared.Enums;
using TheBoys.Shared.Externals.Email;

namespace TheBoys.API.Controllers.Mails;

[Route("api/v1/mail")]
[ApiController]
public class MailController : ControllerBase
{
    readonly IEmailService _emailService;
    readonly EmailSettings _emailSettings;

    public MailController(IEmailService emailService, IOptions<EmailSettings> options)
    {
        _emailService = emailService;
        _emailSettings = options.Value;
    }

    /// <summary>
    ///  send complaint, suggestion or rating
    /// </summary>
    /// <remarks>
    ///     type:
    ///
    ///         [0: complaint, 1: suggestion, 2: rating]
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync(
        [FromBody] SendEmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var response = new Response();

        if (!new EmailAddressAttribute().IsValid(request.Email))
        {
            response.SendBadRequest();
            return Ok(response);
        }

        var body = new StringBuilder(request.Body);

        if (request.Type == MailType.Rating)
        {
            body.AppendLine($"Rating: {request.RatingValue}");
        }

        var mailContract = MailAddressContract.Create(
            _emailSettings.MailHost,
            _emailSettings.MailUsername,
            _emailSettings.MailPasswword,
            _emailSettings.MailPort,
            _emailSettings.MailSender,
            request.Email,
            new List<string>() { "ahmedadel1672003@gmail.com" },
            request.Subject,
            body.ToString(),
            false
        );

        await _emailService.SendEmailAsync(mailContract, cancellationToken);

        response.SendSuccess();
        return Ok(response);
    }
}
