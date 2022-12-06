using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhloSales.Core;
using PhloSales.Data;
using PhloSales.Data.EntityContext;
using PhloSales.Data.Repositories;
using PhloSales.Data.Seeders;

namespace PhloSales.Server.Extensions;

public static class ServiceCollections
{
    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseSeeder, SalesDatabaseSeeder>();
        services.AddDbContext<PhloSalesDbContext>(opt => opt.AddConnectionString());
        return services;
    }

    private static void AddConnectionString(this DbContextOptionsBuilder options)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
            ?? throw new ArgumentNullException($"CONNECTION_STRING env not provided");
        options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging(); // for debugging only
    }

    public static IServiceCollection AddApplicationConfig(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(PhloSalesRepository<>));
        services.AddTransient<IUnitOfWork, PhloSalesUnitOfWork>();
        return services;
    }
}