using MyBackend.ModAuxNomina.BL.Anticipos;
using MyBackend.ModAuxNomina.DA;
using MyBackend.ModAuxNomina.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MyBackend.ModAuxNomina.DA.OracleNedaes;
using MyBackend.ModAuxNomina.BL.OracleDbNedaes;

var builder = WebApplication.CreateBuilder(args);


// Configura la serialización JSON para convertir propiedades a camelCase
builder.Services.AddControllersWithViews()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

string currentDirectory = Directory.GetCurrentDirectory();

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = currentDirectory + "\\wwwroot" // Aquí defines la ruta raíz web deseada
};


// configuración de URLS en las que se va a lanzar el proyecto. Lo suyo sería tenerlo en un appsettings.json
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

//Añadido para permitir el acceso desde la parte frontal
builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("https://*.hacienda.gob.es", "http://localhost:4200", "https://localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

// Agrega servicios al contenedor de servicios de la aplicación
builder.Services.AddControllersWithViews();

//Contextos
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(currentDirectory + "\\appsettings.json", optional: false, reloadOnChange: true)
    .AddCommandLine(args);

// Configura AppSettingsModel para que lea las configuraciones del archivo appsettings.json
builder.Services.Configure<AppSettingsModel>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services.AddDbContext<DbNedaesContext>(options => options.UseSqlServer(builder.Configuration["ApplicationSettings:ConnectionStringDbNedaes"]?.ToString()));
builder.Services.AddDbContext<OracleDbNedaesContext>(options => options.UseOracle(builder.Configuration["ApplicationSettings:ConnectionStringOracleNedaes"]?.ToString()));

// //Añadimos las clases
builder.Services.AddScoped<Anticipos_BL>();
builder.Services.AddScoped<OracleDBNedaes>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
// Añadido para permitir el acceso desde la parte frontal
app.UseCors("AllowSpecificOrigins");

// Mapea las rutas de controlador predeterminadas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// Route all non-file requests to the SPA
app.MapFallbackToFile("index.html");
app.UseStaticFiles();

app.Run();

