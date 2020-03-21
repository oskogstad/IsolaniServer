using Isolani.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Isolani.Database
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class IsolaniDbContext : DbContext 
    {
        public IsolaniDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Game>()
                .Property(game => game.GameState)
                .HasConversion(new EnumToStringConverter<GameState>());
            
            modelBuilder.Entity<User>()
                .Property(user => user.CreatedDateUtc)
                .Metadata
                .SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            modelBuilder.Entity<User>(user => 
            {
                user.HasIndex(usr => usr.Email).IsUnique();
            });
        }

        public DbSet<Game> Games { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<ChessClub> ChessClubs { get; set; }
    }
}
