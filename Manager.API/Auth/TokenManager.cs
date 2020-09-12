using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Manager.API.Auth
{
    public class TokenManager
    {
        public static (string token, string expiration) Create(ClaimsIdentity identity, TokenConfigurations tokenConfigurations)
        {
            var creationDate = DateTime.Now;
            var expirationDate = creationDate.AddSeconds(tokenConfigurations.Seconds);

            var audClaim = identity.FindFirst(JwtRegisteredClaimNames.Aud);
            if (audClaim != null)
                identity.RemoveClaim(audClaim);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = tokenConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });
            string token = handler.WriteToken(securityToken);

            return (token, expirationDate.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        public static Func<TokenValidatedContext, Task> Renew(TokenConfigurations tokenConfigurations)
        {
            return context => {
                context.Response.OnStarting(() =>
                {
                    if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 400)
                    {
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        var (token, expiration) = Create(identity, tokenConfigurations);
                        context.Response.Headers.Add("Set-Token", token);
                        context.Response.Headers.Add("Set-Expiration", expiration);
                    }
                    return Task.CompletedTask;
                });
                return Task.CompletedTask;
            };
        }
    }
}
