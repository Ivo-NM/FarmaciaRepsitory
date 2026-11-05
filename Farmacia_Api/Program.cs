using Farmacia_Api.Data;
using Farmacia_Api.Data.Implementations;
using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Services.ClassServices;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FarmaciaDbContext>(options
    =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configuración para que en caso de fallar de que EF falle en la conversión de un dato a int,
//Muestre este mensaje

builder.Services.AddControllers()
    .AddMvcOptions(options =>
    {
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
            (value, field) => $"El campo {field} debe contener un número entero positivo."
        );
    });

//Configuración para que en caso de fallar de que EF falle en la conversión de un dato a otro,
//Muestre este mensaje

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errores = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors.Select(e =>
            {
                string mensaje = e.ErrorMessage;

                //Verifica si es un texto lo que se envia

                if (mensaje.Contains("System.String"))
                {
                    return $"{x.Key}: Este campo debe enviarse como texto.";
                }

                //Verifica si es un número lo que se envia

                if (mensaje.Contains("System.Int32"))
                {
                    return $"{x.Key}: Este campo debe contener un número entero.";
                }

                return $"{x.Key}: {mensaje}";
            }));

        return new BadRequestObjectResult(new
        {
            Errores = errores
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Inyectando Facturas
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
//Inyectando Entregas
builder.Services.AddScoped<IEntregaRepository, EntregaRepository>();
builder.Services.AddScoped<IEntregaService, EntregaService>();
//Inyectando Inventarios
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

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

app.Run();
