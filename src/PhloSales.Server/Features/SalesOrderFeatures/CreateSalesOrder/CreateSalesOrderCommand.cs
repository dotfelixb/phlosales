namespace PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;

public class CreateSalesOrderCommand 
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
}
