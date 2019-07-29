using Isolani.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Isolani.Database
{
    public class IsolaniDbContext : DbContext 
    {
        public IsolaniDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.CreatedDate)
                .Metadata
                .AfterSaveBehavior = PropertySaveBehavior.Throw;

            modelBuilder.Entity<User>(user => 
            {
                user.HasIndex(usr => usr.Email).IsUnique();
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }
    }
}
