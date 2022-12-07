namespace PhloSales.Server.Features.CustomerFeatures.ListCustomer;

public class ListCustomerQueryResult
{
    public List<CustomerQueryResult> Customers { get; set; }
    public int PageSize { get; set; }
}