using Microsoft.EntityFrameworkCore;
using UpMo.Entities;

namespace UpMo.Data
{
    public partial class UpMoContext : DbContext
    {
        public UpMoContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationManager> OrganizationManagers { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<MonitorBody> MonitorBodys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {

            });

            modelBuilder.Entity<Organization>(entity =>
            {

            });

            modelBuilder.Entity<OrganizationManager>(entity =>
            {

            });

            modelBuilder.Entity<Monitor>(entity =>
            {

            });

            modelBuilder.Entity<MonitorBody>(entity =>
            {

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}