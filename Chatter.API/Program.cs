using System.Reflection.Metadata;
using System.Text;
using Chatter.API.Hubs;
using Chatter.API.Middlewares;
using Chatter.Application.Abstractions;
using Chatter.Application.MappingProfile;
using Chatter.Application.Models;
using Chatter.Application.Services;
using Chatter.Domain.Abstractions;
using Chatter.Domain.Entities;
using Chatter.Infrastructure;
using Chatter.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IDictionary<string, string>>(opt =>
    new Dictionary<string, string>());
        
builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ChatterDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddDbContext<ChatterDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ChatterDbContext))));

builder.Services.AddSignalR()
    .AddAzureSignalR(builder.Configuration["AzureSignalR:ConnectionString"]);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( builder =>
        {
            //builder.WithOrigins("https://reenbittesttaskchat.azurewebsites.net")
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();