using Estudiantes_API_MVC.BLL.Estudiante;
using Estudiantes_API_MVC.DLL.Data;
using Estudiantes_API_MVC.DLL.Repositoio.Estudiante;
using Estudiantes_API_MVC.DLL.Repositorio.Estudiante;
using Microsoft.Data.Sqlite;
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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=../Estudiantes_DB.db";
var sqliteConnectionString = new SqliteConnectionStringBuilder(connectionString);

if (!Path.IsPathRooted(sqliteConnectionString.DataSource))
{
    sqliteConnectionString.DataSource = Path.GetFullPath(
        Path.Combine(builder.Environment.ContentRootPath, sqliteConnectionString.DataSource));
}

// Base de datos con EF Core
builder.Services.AddDbContext<EstudiantesDbContext>(options =>
    options.UseSqlite(sqliteConnectionString.ToString()));

// Inyección de dependencias (repositorio y servicio)
builder.Services.AddScoped<IEstudianteRepositorio, EstudianteRepositorio>();
builder.Services.AddScoped<IEstudianteServicio, EstudianteServicio>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EstudiantesDbContext>();
    dbContext.Database.EnsureCreated();
}

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
