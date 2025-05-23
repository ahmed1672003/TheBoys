using Microsoft.AspNetCore.Http;
using TheBoys.Shared.Abstractions;
using TheBoys.Shared.Enums.Users;

namespace TheBoys.Application.Contexts;

public class UserContext : IUserContext
{
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly HttpContext _httpContext;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpContext = _httpContextAccessor.HttpContext;
    }

    public (bool Exist, Guid Value) Id
    {
        get
        {
            var result = (false, Guid.Empty);

            if (!_httpContext.User.Identity.IsAuthenticated)
            {
                return result;
            }
            var id = _httpContext
                .User.Claims.FirstOrDefault(x => x.Type == nameof(CustomClaimType.UserId))
                ?.Value;

            if (Guid.TryParse(id, out Guid value))
            {
                result.Item1 = true;
                result.Item2 = value;
                return result;
            }
            else
            {
                result.Item1 = true;
                return result;
            }
        }
    }

    public (bool Exist, string Value) Language
    {
        get
        {
            if (_httpContextAccessor is not { HttpContext: null })
            {
                var requestedCulture = _httpContextAccessor
                    .HttpContext.Request.Headers["Accept-Language"]
                    .ToString();

                if (
                    requestedCulture.ToLower() == "ar".ToLower()
                    || requestedCulture.ToLower() == "en".ToLower()
                )
                    return (true, requestedCulture);
                else
                {
                    var language = _httpContextAccessor
                        .HttpContext.User.Claims.FirstOrDefault(x =>
                            x.Type == nameof(CustomClaimType.Language)
                        )
                        ?.Value;
                    if (language is not null)
                        return (false, language);
                }

                return (true, "ar-EG");
            }
            return (false, default);
        }
    }
}
