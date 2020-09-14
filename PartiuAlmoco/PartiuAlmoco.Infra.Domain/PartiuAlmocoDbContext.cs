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
        public DbSet<UserPassword> UserPasswords { get; set; }
        public IAppSettings Settings { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Desativando o tracking automatico do EF para as propriedades do AggregateRoot.
            modelBuilder.Entity<RestaurantPoll>().Ignore(rp => rp.AllRestaurants);
            modelBuilder.Entity<RestaurantPoll>().Ignore(rp => rp.PollResults);
            modelBuilder.Entity<RestaurantPoll>().Ignore(rp => rp.Votes);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// TODO: Descomentar a linha abaixo e o construtor sem parâmetros e rodar os comandos do migration...
            //// dotnet ef migrations add Initial --project PartiuAlmoco.Infra.Domain
            //// dotnet ef database update --project PartiuAlmoco.Infra.Domain
            //optionsBuilder.UseSqlite(@"Data Source=c:\temp\data.sqlite");

            //optionsBuilder.UseSqlite(Settings.DatabaseConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public PartiuAlmocoDbContext(DbContextOptions<PartiuAlmocoDbContext> options) : base(options) { }
        public PartiuAlmocoDbContext() { }
    }
}
