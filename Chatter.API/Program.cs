using Chatter.API;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureService().ConfigurePipeline();

await app.RunAsync();