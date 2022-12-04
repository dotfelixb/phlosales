using AutoMapper;
using PhloSales.Core.Features.CustomerFeatures;
using PhloSales.Core.Features.CustomerFeatures.CreateCustomer;
using PhloSales.Core.Features.CustomerFeatures.GetCustomer;
using PhloSales.Core.Features.ProductFeatures;
using PhloSales.Core.Features.ProductFeatures.CreateProduct;
using PhloSales.Core.Features.ProductFeatures.GetProduct;
using PhloSales.Core.Features.SalesOrderFeatures;
using PhloSales.Core.Features.SalesOrderFeatures.CreateSalesOrder;
using PhloSales.Core.Features.SalesOrderFeatures.GetSalesOrder;
using PhloSales.Data.Entities;

namespace PhloSales.Core;

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