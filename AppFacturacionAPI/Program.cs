using Microsoft.EntityFrameworkCore;
using AppFacturacionAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("FacturacionDB");

// Registra el DbContext para la inyección de dependencias
builder.Services.AddDbContext<FacturacionContext>(options =>
    options.UseSqlServer(connectionString));

// Registra el repositorio genérico con el patrón de inyección de dependencias
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryEF<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();