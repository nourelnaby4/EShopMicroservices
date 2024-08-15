using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc.Protos;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assemply = typeof(Program).Assembly;

// Application service
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(assemply);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


//Grpc Services
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
  .ConfigurePrimaryHttpMessageHandler(() =>
  {
      var handler = new HttpClientHandler
      {
          ServerCertificateCustomValidationCallback =
          HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };

      return handler;
  });

// Data services
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName); //set username as primarykey
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository,BasketRepository>();   
builder.Services.Decorate<IBasketRepository,CacheBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
