using PhloSales.Data.EntityContext;

namespace PhloSales.Data.Seeders;

public class SalesDatabaseSeeder : IDatabaseSeeder
{
    private readonly PhloSalesDbContext _context;

    public SalesDatabaseSeeder(PhloSalesDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        var dbCreated = await _context.Database.EnsureCreatedAsync();
        if (!dbCreated)
        {
            return;
        }

        await SeedData();
    }

    private async Task SeedData()
    {
        _context.Customers.AddRange(new[]
        {
            new Entities.Customer { Name = "Jamie" }, 
            new Entities.Customer { Name = "Anna" },
            new Entities.Customer { Name = "Monica" },
            new Entities.Customer { Name = "Lisa" },
            new Entities.Customer { Name = "Harry" },
        });
        _context.Products.AddRange(new[]
        {
            new Entities.Product { Name = "Dell XPS 13 Laptop" },
            new Entities.Product { Name = "Thinkpad X1 Carbon Laptop" },
            new Entities.Product { Name = "Hp Envy Laptop" },
            new Entities.Product { Name = "Dell Latitute Laptop" },
        });
        await _context.SaveChangesAsync();
    }
}