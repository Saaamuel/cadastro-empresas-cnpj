using Microsoft.EntityFrameworkCore;
using CadastroEmpresasApi.Models;

namespace CadastroEmpresasApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento 1:N entre User e Empresas
            modelBuilder.Entity<User>()
                .HasMany(u => u.Empresas)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
