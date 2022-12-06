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
        _context.Customers.Add(new Entities.Customer { Name = "Jamie" });
        _context.Products.Add(new Entities.Product { Name = "Dell XPS 13 Laptop" });
        await _context.SaveChangesAsync();
    }
}
