using Chatter.Application.Abstractions;
using Chatter.Application.Models;
using Chatter.Application.Services;
using Chatter.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Chatter.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile.MappingProfile));
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<IDictionary<string, Guid>>(opt =>
            new Dictionary<string, Guid>());
        
        return services;
    }
}