using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Infrastructure.Authenticaton;
using CleanArchitecture.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection("Email"));
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}