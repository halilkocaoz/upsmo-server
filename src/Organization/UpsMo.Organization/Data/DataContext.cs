using Microsoft.EntityFrameworkCore;
namespace UpsMo.Organization.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<UpsMo.Organization.Data.Models.Organization> Organizations { get; set; }
    public DbSet<UpsMo.Organization.Data.Models.Manager> Managers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}