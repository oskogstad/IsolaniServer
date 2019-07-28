using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using foo_chess_server.Domain;

namespace foo_chess_server.Database
{
    public class FooChessContext : DbContext 
    {
        public FooChessContext(DbContextOptions options) : base(options) { }

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
    }
}
