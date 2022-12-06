using AutoMapper;
using PhloSales.Server.Features.CustomerFeatures.GetCustomer;
using PhloSales.Data.Entities;
using PhloSales.Server.Features.CustomerFeatures;
using PhloSales.Server.Features.CustomerFeatures.CreateCustomer;
using PhloSales.Server.Features.ProductFeatures;
using PhloSales.Server.Features.ProductFeatures.CreateProduct;
using PhloSales.Server.Features.ProductFeatures.GetProduct;
using PhloSales.Server.Features.SalesOrderFeatures;
using PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;
using PhloSales.Server.Features.SalesOrderFeatures.GetSalesOrder;

public class PhloSalesServer
{

}

public class ModelProfiles : Profile
{
    public ModelProfiles()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Customer, GetCustomerQueryResult>();
        CreateMap<Customer, CustomerQueryResult>();
        CreateMap<Product, GetProductQueryResult>();
        CreateMap<Product, ProductQueryResult>();
        CreateMap<CreateSalesOrderCommand, SalesOrder>();
        CreateMap<SalesOrder, GetSalesOrderQueryResult>()
            .ForMember(d => d.Customer, opt => opt.MapFrom(s => s.Customer.Name ?? ""))
            .ForMember(d => d.Product, opt => opt.MapFrom(s => s.Product.Name ?? "")); ;
        CreateMap<SalesOrder, SalesOrderQueryResult>()
            .ForMember(d => d.Customer, opt => opt.MapFrom(s => s.Customer.Name ?? ""))
            .ForMember(d => d.Product, opt => opt.MapFrom(s => s.Product.Name ?? ""));
    }
}