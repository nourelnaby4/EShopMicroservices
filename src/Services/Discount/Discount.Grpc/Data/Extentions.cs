using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extentions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbcontext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
        dbcontext.Database.MigrateAsync();

        return app;
    }
}
