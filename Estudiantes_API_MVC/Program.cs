using Estudiantes_API_MVC.BLL.Estudiante;
using Estudiantes_API_MVC.DLL.Data;
using Estudiantes_API_MVC.DLL.Repositoio.Estudiante;
using Estudiantes_API_MVC.DLL.Repositorio.Estudiante;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Controladores
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Estudiantes API", Version = "v1" });
});

// Base de datos con EF Core (ejemplo usando SQLite, puedes cambiar a SQL Server)
builder.Services.AddDbContext<EstudiantesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias (repositorio y servicio)
builder.Services.AddScoped<IEstudianteRepositorio, EstudianteRepositorio>();
builder.Services.AddScoped<IEstudianteServicio, EstudianteServicio>();

var app = builder.Build();

// Middleware de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estudiantes API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();