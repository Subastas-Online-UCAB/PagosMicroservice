using Microsoft.EntityFrameworkCore;
using Pagos.Infrastructure.Data;
using Pagos.Infrastructure.Servicios;
using Pagos.Application.Servicios;
using Pagos.Domain.Interfaces;
using Pagos.Infrastructure.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentRepositorio, PaymentRepositorio>();
builder.Services.AddScoped<StripeServicio>();
builder.Services.AddScoped<IPaymentServicio, PaymentServicio>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Aplicar migraciones automáticamente (solo para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PagoDbContext>();
    dbContext.Database.Migrate();
}

app.Run();