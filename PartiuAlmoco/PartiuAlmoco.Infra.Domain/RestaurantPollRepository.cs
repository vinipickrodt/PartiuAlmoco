using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Linq;

namespace PartiuAlmoco.Infra.Domain
{
    public class RestaurantPollRepository : IRestaurantPollRepository
    {
        public PartiuAlmocoDbContext dbContext = null;
        private IRestaurantRepository restaurantRepository;

        public RestaurantPollRepository(PartiuAlmocoDbContext dbContext, IRestaurantRepository restaurantRepository)
        {
            Guard.Against.Null(dbContext, nameof(dbContext));
            Guard.Against.Null(restaurantRepository, nameof(restaurantRepository));

            this.dbContext = dbContext;
            this.restaurantRepository = restaurantRepository;
        }

        public RestaurantPoll GetPollByDate(DateTime date)
        {
            if (date <= DateTime.MinValue)
            {
                throw new ArgumentNullException(nameof(date));
            }

            var poll = dbContext.RestaurantPolls
                .Where(poll => poll.Date.Date == date.Date)
                .FirstOrDefault();

            if (poll == null)
            {
                return null;
            }

            // busca os ultimos 100 resultados das votações passadas.
            var pollResults = dbContext.PollResults
                .Where(pr => pr.RestaurantPoll.Id == poll.Id && pr.Date.Date <= date.Date)
                .OrderByDescending(pr => pr.Date)
                .Include(v => v.WinnerRestaurant)
                .Include(v => v.RestaurantPoll)
                .Take(100).ToList();
            poll.SetPollResults(pollResults);

            // busca todos os votos (máximo = 5000).
            var votes = dbContext.Votes
                .Where(v => v.RestaurantPoll.Id == poll.Id)
                .Include(v => v.Restaurant)
                .Include(v => v.Voter)
                .Include(v => v.RestaurantPoll)
                .Take(5000).ToList();
            poll.SetVotes(votes);

            return poll;
        }

        public void Confirm(RestaurantPoll poll)
        {
            dbContext.Attach(poll);

            // salva no banco de dados o(s) voto(s) adicionado(s).
            dbContext.AttachRange(poll.Votes);

            // salva no banco de dados o(s) resultado(s) adicionado(s).
            dbContext.AttachRange(poll.PollResults);

            dbContext.SaveChanges();
        }

        public RestaurantPoll NewPoll(string name, DateTime date)
        {
            var restaurantPoll = new RestaurantPoll(Guid.NewGuid(), name, date, restaurantRepository.GetAllRestaurants(), null);
            dbContext.RestaurantPolls.Add(restaurantPoll);
            dbContext.SaveChanges();
            return restaurantPoll;
        }

        public void Add(RestaurantPoll poll)
        {
            dbContext.RestaurantPolls.Add(poll);
            dbContext.SaveChanges();
        }
    }
}
