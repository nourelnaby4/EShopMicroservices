using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

var assemply = typeof(Program).Assembly;
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(assemply);
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assemply);

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName); //set username as primarykey
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
