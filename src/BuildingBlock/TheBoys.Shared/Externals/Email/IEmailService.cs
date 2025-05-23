namespace TheBoys.Shared.Externals.Email;

public interface IEmailService
{
    Task<bool> SendEmailAsync(
        MailAddressContract mailContract,
        CancellationToken cancellationToken = default
    );
}
