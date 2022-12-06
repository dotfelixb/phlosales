namespace PhloSales.Server.Features.DashboardFeature;

public class SalesUnitQueryReesult
{
    public int ProductId { get; set; }
    public string Product { get; set; } = null!;
    public int Unit { get; set; }
    public decimal Gross { get; set; }
    public decimal HighestPrice { get; set; }
    public decimal LowestPrice { get; set; }
}