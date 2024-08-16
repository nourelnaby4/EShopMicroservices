namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceProvider AddApiServices(this IServiceProvider services)
    {
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}
