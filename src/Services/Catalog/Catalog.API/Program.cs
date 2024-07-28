using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var assemply = typeof(Program).Assembly;
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(assemply);
    configuration.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});

builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
