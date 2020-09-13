using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartiuAlmoco.Core.Domain.Interfaces;
using PartiuAlmoco.Infra.Domain;
using System;
using System.Linq;

namespace PartiuAlmoco.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<PartiuAlmocoDbContext>()
                .AddTransient<IRestaurantPollRepository, RestaurantPollRepository>()
                .AddTransient<IAppSettings, AppSettings>()
                .BuildServiceProvider();

            var repo = (RestaurantPollRepository)serviceProvider.GetService<IRestaurantPollRepository>();
            var poll = repo.GetPollByDate(DateTime.Now);

            var user = repo.dbContext.Users.First();
            var restaurant = repo.dbContext.Restaurants.First();

            poll.AddVote(restaurant, user);
            repo.Confirm(poll);

            Console.WriteLine("Hello World!");
        }
    }
}
