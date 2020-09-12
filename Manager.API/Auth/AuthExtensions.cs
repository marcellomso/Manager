using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Manager.API.Auth
{
    public static class AuthExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurations = configuration.GetSection(nameof(TokenConfigurations)).Get<TokenConfigurations>();
            if (tokenConfigurations.SigningCredentials == null)
                tokenConfigurations.SecurityKey = "";
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters.ValidateIssuer = true;
                options.TokenValidationParameters.ValidIssuer = tokenConfigurations.Issuer;

                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.ValidAudience = tokenConfigurations.Audience;

                options.TokenValidationParameters.IssuerSigningKey = tokenConfigurations.SigningCredentials.Key;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;

                options.TokenValidationParameters.ValidateLifetime = true;

                options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = TokenManager.Renew(tokenConfigurations)
                };

            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void AddAuthK1(this IServiceCollection services, IConfiguration configuration)
        {
            var securityKey = configuration["SecurityKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.IssuerSigningKey = signingKey;

                paramsValidation.ValidateIssuer = true;
                paramsValidation.ValidIssuer = "bitzenManager";

                paramsValidation.ValidateAudience = true;
                paramsValidation.ValidAudience = "http://bitzen.tech/";

                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
