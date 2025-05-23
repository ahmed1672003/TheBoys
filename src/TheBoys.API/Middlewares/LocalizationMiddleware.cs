using System.Globalization;
using TheBoys.Shared.Abstractions;

namespace TheBoys.API.Middlewares;

public sealed class LocalizationMiddleWare : IMiddleware
{
    readonly IUserContext _userContext;

    public LocalizationMiddleWare(IUserContext userContext) => _userContext = userContext;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_userContext.Language.Exist)
        {
            var culture = new CultureInfo(_userContext.Language.Value);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
        await next(context);
    }
}
