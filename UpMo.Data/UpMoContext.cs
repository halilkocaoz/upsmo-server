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
        public DbSet<MonitorPostBody> MonitorPostBodies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasOne<AppUser>().WithMany().HasForeignKey(organization => organization.CreatorUserID);
            });

            modelBuilder.Entity<OrganizationManager>(entity =>
            {
                entity.HasOne<Organization>()
                .WithMany(organization => organization.Managers)
                .HasForeignKey(organizationManager => organizationManager.OrganizationID);

                entity.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(organizationManager => organizationManager.UserID);
            });

            modelBuilder.Entity<Monitor>(entity =>
            {
                entity.HasOne<Organization>()
                .WithMany(organization => organization.Monitors)
                .HasForeignKey(monitor => monitor.OrganizationID);
            });

            modelBuilder.Entity<MonitorPostBody>(entity =>
            {
                entity.HasOne<Monitor>()
                .WithMany(monitor => monitor.PostBodyValues)
                .HasForeignKey(monitorpostbody => monitorpostbody.MonitorID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}