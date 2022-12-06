using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PhloSales.Data;

namespace PhloSales.Core.Extensions;

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
