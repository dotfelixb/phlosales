using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace PhloSales.Core.Extensions;

public static class ServiceCollections
{
   public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services)
    {
        var bearerKey = Environment.GetEnvironmentVariable("BEARER_KEY")
             ?? throw new ArgumentNullException($"BEARER_KEY env not provided");

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata= false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };
        });
        services.AddAuthorization(o => o.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection AddFluentValidationConfig<T>(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<T>(includeInternalTypes: true);
        return services;
    }

    public static IServiceCollection AddMediatRConfig<T>(this IServiceCollection services)
    { 
        services.AddMediatR(new Assembly[] { typeof(T).Assembly });
        return services;
    }

    public static IServiceCollection AddAutoMapperConfig<T>(this IServiceCollection services)
    {
        services.AddAutoMapper(new Assembly[] { typeof(T).Assembly });
        return services;
    }
}
