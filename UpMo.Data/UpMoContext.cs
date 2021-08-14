using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpMo.Entities;

namespace UpMo.Data
{
    public partial class UpMoContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public UpMoContext(DbContextOptions options) : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationManager> OrganizationManagers { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<PostFormData> PostFormData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(props => props.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasOne<AppUser>().WithMany().HasForeignKey(organization => organization.CreatorUserID);
            });

            modelBuilder.Entity<OrganizationManager>(entity =>
            {
                entity.HasOne(organizationManager => organizationManager.Organization)
                .WithMany(organization => organization.Managers)
                .HasForeignKey(organizationManager => organizationManager.OrganizationID);

                entity.HasOne(organizationManager => organizationManager.User)
                .WithMany()
                .HasForeignKey(organizationManager => organizationManager.UserID);
            });

            modelBuilder.Entity<Monitor>(entity =>
            {
                entity.HasOne(monitor => monitor.Organization)
                .WithMany(organization => organization.Monitors)
                .HasForeignKey(monitor => monitor.OrganizationID);
            });

            modelBuilder.Entity<PostFormData>(entity =>
            {
                entity.HasOne<Monitor>()
                .WithMany(monitor => monitor.PostFormData)
                .HasForeignKey(monitorpostbody => monitorpostbody.MonitorID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}