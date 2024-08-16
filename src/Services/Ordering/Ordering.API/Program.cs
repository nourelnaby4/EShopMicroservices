using Ordering.Application;
using Ordering.API;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add service to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();
