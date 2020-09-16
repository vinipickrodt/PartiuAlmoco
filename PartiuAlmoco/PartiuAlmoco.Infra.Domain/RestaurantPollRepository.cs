using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

            LoadPoll(poll);

            return poll;
        }

        private RestaurantPoll LoadPoll(RestaurantPoll poll)
        {
            // busca os ultimos 100 resultados das votações passadas.
            var pollResults = dbContext.PollResults
                .Where(pr => pr.Date.Date <= poll.Date.Date)
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

            poll.SetRestaurants(dbContext.Restaurants.ToList());

            return poll;
        }

        public void Confirm(RestaurantPoll poll)
        {
            dbContext.Attach(poll);

            // salva no banco de dados o(s) voto(s) adicionado(s).
            dbContext.AttachRange(poll.Votes);

            dbContext.SaveChanges();
        }

        public RestaurantPoll NewPoll(string name, DateTime date)
        {
            var restaurantPoll = new RestaurantPoll(Guid.NewGuid(), name, date, restaurantRepository.GetAllRestaurants());
            dbContext.RestaurantPolls.Add(restaurantPoll);
            dbContext.SaveChanges();
            return restaurantPoll;
        }

        public void Add(RestaurantPoll poll)
        {
            dbContext.RestaurantPolls.Add(poll);
            dbContext.SaveChanges();
        }

        public IEnumerable<RestaurantPoll> GetUnfinishedPolls(DateTime date)
        {
            return dbContext.RestaurantPolls
                .Where(poll => poll.WinnerRestaurant == null && poll.Date < date.Date)
                .ToList()
                .Select(poll => LoadPoll(poll));
        }

        public RestaurantPoll GetPollById(Guid id)
        {
            var poll = dbContext.RestaurantPolls.Find(id);

            if (poll == null)
                return null;

            return LoadPoll(poll);
        }

        public void AddPollResult(RestaurantPollResult result)
        {
            dbContext.PollResults.Add(result);
            dbContext.SaveChanges();
        }
    }
}
