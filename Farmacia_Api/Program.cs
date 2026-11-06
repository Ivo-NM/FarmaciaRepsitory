using Farmacia_Api.Data;
using Farmacia_Api.Data.Implementations;
using Farmacia_Api.Data.Interfaces;
using Farmacia_Api.Services.ClassServices;
using Farmacia_Api.Services.InterfacesService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresá el JWT (sin el bearer)."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            [""]
        }
    });
});

//Inyectando Facturas
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
//Inyectando Entregas
builder.Services.AddScoped<IEntregaRepository, EntregaRepository>();
builder.Services.AddScoped<IEntregaService, EntregaService>();
//Inyectando Inventarios
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
//inyectando movimientos
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();
//inyectando clientes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
//inyectando medicamentos
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();
//inyectando productos
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
//inyectando sucursales
builder.Services.AddScoped<ISucursalRepository, SucursalRepository>();
builder.Services.AddScoped<ISucursalService, SucursalService>();
//inyectando empleados
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
//inyectando usuarios
builder.Services.AddScoped<IUserRepository, UserRepository>();


var key = builder.Configuration["JwtSettings:Key"];
Console.WriteLine($"CLAVE LEÍDA: '{key}'");
Console.WriteLine($"ENTORNO ACTUAL: {builder.Environment.EnvironmentName}");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Farmacia_api",
            ValidAudience = "Farmacia_Frontend",
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
        };
    });






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


/*
 * builder.Services.AddSwaggerGen(options => {
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
{
Name = "Authorization",
Type = SecuritySchemeType.Http,
Scheme = "bearer",
BearerFormat = "JWT",
In = ParameterLocation.Header,
Description = "Ingresá el JWT (sin el bearer)."
});

options.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
{
new OpenApiSecurityScheme()
{
Reference = new OpenApiReference()
{
Type = ReferenceType.SecurityScheme,
Id = "Bearer"
}
},
[""]
}
});
}
);
 */
