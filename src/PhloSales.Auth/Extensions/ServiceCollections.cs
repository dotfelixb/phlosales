using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhloSales.Data;
using PhloSales.Data.Entities;
using PhloSales.Data.EntityContext;
using PhloSales.Data.Repositories;
using PhloSales.Data.Seeders;

namespace PhloSales.Auth.Extensions;

public static class ServiceCollections
{
    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseSeeder, AuthDatabaseSeeder>();
        services.AddDbContext<PhloAuthDbContext>(opt => opt.AddConnectionString());
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
        services.AddTransient(typeof(IRepository<>), typeof(PhloAuthRepository<>));
        services.AddTransient<IUnitOfWork, PhloAuthUnitOfWork>();
        return services;
    }

    public static IServiceCollection AddIdentityUserConfig(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
        }).AddEntityFrameworkStores<PhloAuthDbContext>().AddDefaultTokenProviders();
        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(48));
        return services;
    }
}