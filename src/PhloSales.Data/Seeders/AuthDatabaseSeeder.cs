using Microsoft.AspNetCore.Identity;
using PhloSales.Data.Entities;
using PhloSales.Data.EntityContext;

namespace PhloSales.Data.Seeders;

public class AuthDatabaseSeeder : IDatabaseSeeder
{
    private readonly PhloAuthDbContext _context;

    public AuthDatabaseSeeder(PhloAuthDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var dbCreated = await _context.Database.EnsureCreatedAsync();
            if (!dbCreated)
            {
                return;
            }

            await SeedData();
        }
        catch { /* don't care about the exception at the moment */  }
    }

    private async Task SeedData()
    {
        var email = "admin@phlosales.com";
        var password = "1433P@$$";
        var hasher = new PasswordHasher<ApplicationUser>();

        var adminUser = new ApplicationUser
        {
            Email =  email,
            UserName = email,
            NormalizedUserName = email.ToUpper(),
            EmailConfirmed = true,
            NormalizedEmail = email.ToUpper(),
            PhoneNumberConfirmed = true,
            SecurityStamp = string.Empty,
        };

        adminUser.PasswordHash = hasher.HashPassword(adminUser, password);
        _context.Users.Add(adminUser);
        await _context.SaveChangesAsync();
    }
}