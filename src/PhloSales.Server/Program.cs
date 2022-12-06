using PhloSales.Core.Extensions;
using PhloSales.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationConfig<PhloSalesServer>();
builder.Services.AddMediatRConfig<PhloSalesServer>();
builder.Services.AddAutoMapperConfig<PhloSalesServer>();
builder.Services.AddDatabaseConfig();
builder.Services.AddApplicationConfig();
builder.Services.AddAuthenticationConfig();
builder.Services.AddCors();

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
