namespace PhloSales.Data.Entities;

public class Product : Entity
{
    public string Name { get; set; } = null!;
    public ICollection<SalesOrder> SalesOrders { get; set; } = null!;
}