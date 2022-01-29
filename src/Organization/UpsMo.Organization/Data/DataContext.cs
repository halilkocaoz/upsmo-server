using Microsoft.EntityFrameworkCore;
namespace UpsMo.Organization.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Models.Organization> Organizations { get; set; }
    public DbSet<Models.Manager> Managers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}