namespace PhloSales.Data.Entities;

public class SalesOrder : Entity
{
    public int CustomerId { get; set; } 
    public Customer Customer { get; set; } = null!;
    public int ProductId { get; set; } 
    public Product Product { get; set; } = null!;
    public decimal Price { get; set; }
}
