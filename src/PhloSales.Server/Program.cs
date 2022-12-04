using PhloSales.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationConfig();
builder.Services.AddMediatRConfig();
builder.Services.AddAutoMapperConfig();
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddApplicationConfig();
builder.Services.AddCors();

var app = builder.Build();
await app.InitializeSeedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(o => true).AllowCredentials());
app.UseAuthorization();
app.MapControllers();

app.Run();