using System.Reflection.Metadata;
using System.Text;
using Chatter.API;
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

var app = builder.ConfigureService().ConfigurePipeline();

await app.RunAsync();