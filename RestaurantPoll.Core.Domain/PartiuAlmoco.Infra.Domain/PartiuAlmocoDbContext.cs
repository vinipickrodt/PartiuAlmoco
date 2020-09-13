using Microsoft.EntityFrameworkCore;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System.IO;

namespace PartiuAlmoco.Infra.Domain
{
    public class PartiuAlmocoDbContext : DbContext
    {
        public DbSet<RestaurantPoll> RestaurantPolls { get; set; }
        public DbSet<RestaurantPollResult> PollResults { get; set; }
        public DbSet<RestaurantPollVote> Votes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dir = Directory.GetCurrentDirectory();
            var dbPath = Path.Combine(dir, "data.sqlite");
            
            optionsBuilder.UseSqlite(@$"Data Source={dbPath}");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
