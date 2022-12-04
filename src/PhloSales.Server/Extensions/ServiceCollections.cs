using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PhloSales.Core;
using PhloSales.Data;

namespace PhloSales.Server.Extensions;

public static class ServiceCollections
{
    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
        services.AddDbContext<PhloSalesDbContext>(opt => opt.AddConnectionString(configuration));
        return services;
    }
    
    private static void AddConnectionString(this DbContextOptionsBuilder options,
        IConfiguration configuration)
    {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
    }
    
    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IPhloSalesCore>(includeInternalTypes: true);
        return services;
    }

    public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
    {
        services.AddMediatR(new[] { typeof(IPhloSalesCore) });
        return services;
    }

    public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(new[] { typeof(IPhloSalesCore) });
        return services;
    }

    public static IServiceCollection AddApplicationConfig(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}

