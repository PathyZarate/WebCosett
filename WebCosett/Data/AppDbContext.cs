using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebCosett.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Archivo> Archivo { get; set; }
        public DbSet<RegistroLinea> RegistroLinea { get; set; }
        public DbSet<DatoRegistro> DatoRegistro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
