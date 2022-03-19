using Microsoft.EntityFrameworkCore;
using TiendaEnLinea.Api.Data;
using TiendaEnLinea.Api.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Se agrega a la api el data context
// Se especifican las opciones
// Para crear la migración: dotnet ef migrations add Initial (Se corre desde la consola del administrador de paquetes)
// y estando en la carpeta del proyecto
// Para correr la migración: dotnet ef database update o update-database
// Se puede deshacer una migración usando update-database 0
builder.Services.AddDbContextPool<TiendaEnLineaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaEnLineaConnection")) // Se indica la cadena de conexión
);

// Se registra el repositorio para que pueda ser usado por inyeccion de dependencias. Con la opcion scoped
// Se determina que la misma instancia del objeto es inyectada entre las clases relevantes dentro de un peticion http particular
// Una nueva instancia del objeto relevante sera creada para cada peticion http
builder.Services.AddScoped<IProductRepository, ProductRepository>();
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
