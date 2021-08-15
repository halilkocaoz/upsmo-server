using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UpMo.Entities;

namespace UpMo.Data
{
    public partial class UpMoContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public UpMoContext(DbContextOptions options) : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Manager> Managers { get; set; }
        
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<PostForm> PostForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(props => props.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);

                entity.HasOne<AppUser>().WithMany().HasForeignKey(organization => organization.CreatorUserID);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);

                entity.HasOne(manager => manager.Organization)
                .WithMany(organization => organization.Managers)
                .HasForeignKey(manager => manager.OrganizationID);

                entity.HasOne(manager => manager.User)
                .WithMany()
                .HasForeignKey(manager => manager.UserID);
            });

            modelBuilder.Entity<Monitor>(entity =>
            {
                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);

                entity.HasOne(monitor => monitor.Organization)
                .WithMany(organization => organization.Monitors)
                .HasForeignKey(monitor => monitor.OrganizationID);
            });
            
            modelBuilder.Entity<Header>(entity =>
            {
                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
                
                entity.HasOne(x => x.Monitor)
                .WithMany(monitor => monitor.Headers)
                .HasForeignKey(x => x.MonitorID);
            });

            modelBuilder.Entity<PostForm>(entity =>
            {
                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);

                entity.HasOne(x => x.Monitor)
                .WithMany(monitor => monitor.PostForms)
                .HasForeignKey(x => x.MonitorID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}