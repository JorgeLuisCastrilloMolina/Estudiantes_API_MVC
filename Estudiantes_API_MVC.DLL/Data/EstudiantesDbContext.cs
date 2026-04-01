using Estudiantes_API_MVC.DLL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Estudiantes_API_MVC.DLL.Data
{
    public partial class EstudiantesDbContext : DbContext
    {
        public EstudiantesDbContext(DbContextOptions<EstudiantesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.ToTable("ESTUDIANTES");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}