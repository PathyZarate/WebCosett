using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebCosett.Models;
using DocumentFormat.OpenXml.InkML;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace WebCosett.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<RegistroLinea> RegistroLineas { get; set; }
        public DbSet<DatoRegistro> DatoRegistros { get; set; }
    }
}