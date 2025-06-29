using TheBoys.Application.Features.ContactUs.Commands.SendEmail;

namespace TheBoys.API.Controllers;

[Route("api/v1/mail")]
[ApiController]
public class MailController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///  send complaint, suggestion or rating
    /// </summary>
    /// <remarks>
    ///     type:
    ///
    ///         [0: complaint, 1: suggestion, 2: rating]
    /// </remarks>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync(
        [FromBody] SendEmailCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await mediator.Send(command, cancellationToken));
}
