namespace TheBoys.Application.PipelineBehaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var context = new ValidationContext<TRequest>(request);

        if (_validators.Any())
        {
            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context))
            );

            var failures = validationFailures.SelectMany(vr => vr.Errors).Where(f => f != null);

            if (failures is not null && failures.Any())
            {
                var message = failures.Select(vf => vf.ErrorMessage).FirstOrDefault();
                throw new ValidationException(message, failures);
            }
        }
        return await next();
    }
}
