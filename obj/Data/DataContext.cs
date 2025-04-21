using Microsoft.EntityFrameworkCore;
using CadastroEmpresas.API.Models;

namespace CadastroEmpresas.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
}
