using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TheBoys.Application.Abstractions;
using TheBoys.Application.Settings;
using TheBoys.Domain.Entities.Users;
using TheBoys.Shared.Enums.Users;
using TheBoys.Shared.Extensions;

namespace TheBoys.Infrastructure.Jwt;

public sealed class JwtManager : IJwtManager
{
    readonly IOptions<JwtSettings> _jwtSettingsoptions;
    readonly JwtSettings _jwtSettings;

    public JwtManager(IOptions<JwtSettings> jwtSettingsoptions)
    {
        _jwtSettingsoptions = jwtSettingsoptions;
        _jwtSettings = jwtSettingsoptions.Value;
    }

    public Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>()
        {
            new(nameof(CustomClaimType.Username), user.Username),
            new(nameof(CustomClaimType.Email), user.Email),
            new(nameof(CustomClaimType.Role), user.Role.Type.ToString()),
        };
        if (user.Phone.HasValue())
            claims.Add(new(nameof(CustomClaimType.Phone), user.Phone));

        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_jwtSettings.Secret)
        );
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(_jwtSettings.AccessTokenExpireDate),
            signingCredentials: new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return Task.FromResult(accessToken);
    }
}
