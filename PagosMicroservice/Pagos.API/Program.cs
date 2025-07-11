using MediatR;
using Pagos.Application.Commands;
using Pagos.Domain.Repositorios;
using Pagos.Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using Pagos.Infrastructure.Persistencia;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MassTransit;
using Pagos.Application.Sagas;
using Pago.Infrastructure.Configuracion;
using Pagos.Infrastructure.Mongo;
using Pagos.Infrastructure.MongoDB;
using Pagos.Infrastructure.Consumidor;
using Pagos.Domain.Interfaces;
using Pagos.Infrastructure.EventPublisher;
using Pagos.Application.servicios;
using Pago.Infrastructure.Persistencia;






var builder = WebApplication.CreateBuilder(args);

//Swagger
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRequestHandler<CrearPagoCommand, Guid>, CrearPagoCommandHandler>();
builder.Services.AddScoped<PagoActualizadoConsumidor>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPagoRepository, PagoRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CrearPagoCommand).Assembly));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var keycloak = builder.Configuration.GetSection("Keycloak");
        options.Authority = "http://localhost:8081/realms/microservicio-usuarios";
        options.Audience = "account";
        options.RequireHttpsMetadata = false; // solo si estás en desarrollo local
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PagoService.Api",
        Version = "v1"
    });

    // Configuración de seguridad JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingresa el token JWT como: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


//Mongo 

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();

// MassTransit
builder.Services.AddMassTransit(x =>
{
    // 1. Registrar consumidores
    x.AddConsumer<PagoCreadoConsumidor>();
    x.AddConsumer<PagoActualizadoConsumidor>(); // 👈 Nuevo consumer agregado

    // 2. Registrar la saga
    x.AddSagaStateMachine<PagoStateMachine, PagoState>()
        .MongoDbRepository(r =>
        {
            r.Connection = builder.Configuration["MongoSettings:ConnectionString"];
            r.DatabaseName = builder.Configuration["MongoSettings:DatabaseName"];

            r.CollectionName = "pago_sagas"; // opcional
        });


    // 3. Configurar RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => { });

        // Consumer para evento SubastaCreada
        cfg.ReceiveEndpoint("pago-creado-evento", e =>
        {
            e.ConfigureConsumer<PagoCreadoConsumidor>(context);
        });

        cfg.ReceiveEndpoint("pago-actualizado-evento", e =>
        {
            e.ConfigureConsumer<PagoActualizadoConsumidor>(context);
        });

        // Endpoint para la saga
        cfg.ConfigureEndpoints(context);
    });
});


builder.Services.AddScoped<IPublicadorPagoEventos, PublicadorPagoEventos>();
builder.Services.AddSingleton<IPagoMongoContext, MongoDbContext>();
builder.Services.AddScoped<IMongoPagoRepository, MongoPagoRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run()
;