using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace Data {
    public class Context : DbContext {
        private string cadenaConexion;
        public DbSet<Usuarios> Usuarios {get; set;}
        public DbSet<Roles> Roles {get; set;}
        public DbSet<Articulos> Articulos {get; set;}
        public DbSet<ArticuloXPedido> ArticuloXPedido {get; set;}
        public DbSet<Clientes> Clientes {get; set;}
        public DbSet<Facturas> Facturas {get; set;}
        public DbSet<FormasDePago> FormasDePago {get; set;}
        public DbSet<Pedidos> Pedidos {get; set;}
        public DbSet<Proveedores> Proveedores {get; set;}
        public DbSet<Tarjetas> Tarjetas {get; set;}
        public DbSet<Zonas> Zonas {get; set;}


        public Context(DbContextOptions options) : base(options) {

        }
        public Context() : base() {
            var configuration = GetConfiguration();
            cadenaConexion = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
        public IConfigurationRoot GetConfiguration() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql(cadenaConexion);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}