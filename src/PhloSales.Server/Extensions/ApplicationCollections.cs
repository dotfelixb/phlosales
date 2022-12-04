using PhloSales.Data;

namespace PhloSales.Server.Extensions;

public static class ApplicationCollections 
{
    public static async Task InitializeSeedAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();
        foreach (var initializer in initializers)
        {
           await initializer.InitializeAsync();
        }
    }
}