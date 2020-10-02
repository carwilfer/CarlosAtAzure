using Domain;
using Domain.Estado;
using Domain.Pais;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Repository
{
    public class WebApiAmigoContext : DbContext
    {
        public WebApiAmigoContext (DbContextOptions<WebApiAmigoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmigosDoAmigo>().ToTable("AmigoDoAmigo");

        }

        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<AmigosDoAmigo> AmigosDosAmigos { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }

        public class BloggingContextFactory : IDesignTimeDbContextFactory<WebApiAmigoContext>
        {
            public WebApiAmigoContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<WebApiAmigoContext>();
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebApiAmigoContext-99f04b7d-3e17-45fe-90cb-e9cc925b4579;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                return new WebApiAmigoContext(optionsBuilder.Options);
            }
        }
    }
}
