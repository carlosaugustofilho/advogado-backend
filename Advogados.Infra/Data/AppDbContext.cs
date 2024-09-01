using Microsoft.EntityFrameworkCore;
using Advogados.Domain.Entities;

namespace Advogados.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Advogado> Advogados { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamento de enum Estado para int
            modelBuilder.Entity<Endereco>()
                .Property(e => e.Estado)
                .HasConversion<int>();

            modelBuilder.Entity<Advogado>()
                .HasMany(a => a.Enderecos)
                .WithOne(e => e.Advogado)
                .HasForeignKey(e => e.AdvogadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
