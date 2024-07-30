using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Chatter.API.Hubs;
using Chatter.API.Middlewares;
using Chatter.Application;
using Chatter.Domain.Entities;
using Chatter.Infrastructure;
using Chatter.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.IdentityModel.Tokens;

namespace Chatter.API;

public static class StartupHelperExtensions
{
    public static WebApplication ConfigureService(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();

        builder.Services.AddApplicationService();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        var keyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultUrl");
        var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
        var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");

        builder.Configuration.AddAzureKeyVault(keyVaultUrl.Value!, keyVaultClientId.Value!,
            keyVaultClientSecret.Value!, new DefaultKeyVaultSecretManager());

        builder.Services.AddSignalR()
            .AddAzureSignalR(KeyVaultExtension.GetConnectionSecret(builder.Configuration,"AzureSignalR"));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("https://reenbittesttaskchat.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<ChatHub>("/chat");

        return app;
    }
}