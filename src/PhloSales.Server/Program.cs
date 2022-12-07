using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PhloSales.Core;
using PhloSales.Core.Extensions;
using PhloSales.Server.Extensions;
using PhloSales.Server.Features.SalesOrderFeatures.CreateSalesOrder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(o => o.Filters.Add<ValidationFilter>()).AddJsonOptions(o => { });
builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationConfig<PhloSalesServer>();
builder.Services.AddMediatRConfig<PhloSalesServer>();
builder.Services.AddAutoMapperConfig<PhloSalesServer>();
builder.Services.AddDatabaseConfig();
builder.Services.AddApplicationConfig();
builder.Services.AddAuthenticationConfig();
builder.Services.AddCors();
builder.Services.AddScoped<IValidator<CreateSalesOrderCommandList>, CreateSalesOrderCommandListValidator>();

var app = builder.Build();
await app.InitializeSeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(o => true).AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();