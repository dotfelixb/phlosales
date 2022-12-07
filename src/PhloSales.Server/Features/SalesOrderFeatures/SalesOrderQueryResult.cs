namespace PhloSales.Server.Features.SalesOrderFeatures;

public class SalesOrderQueryResult
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Customer { get; set; } = null!;
    public int ProductId { get; set; }
    public string Product { get; set; } = null!;
    public decimal Price { get; set; }
}