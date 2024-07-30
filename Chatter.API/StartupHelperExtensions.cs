using System.Text;
using Chatter.API.Hubs;
using Chatter.API.Middlewares;
using Chatter.Application;
using Chatter.Domain.Entities;
using Chatter.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        
        builder.Services.AddSignalR()
            .AddAzureSignalR(builder.Configuration["AzureSignalR:ConnectionString"]);

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