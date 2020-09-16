using Ardalis.GuardClauses;
using PartiuAlmoco.Core.Domain.DTO;
using PartiuAlmoco.Core.Domain.Interfaces;
using PartiuAlmoco.Core.Domain.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate
{
    public class RestaurantPoll : Entity, IAggregateRoot
    {
        private List<Restaurant> _allRestaurants = new List<Restaurant>();
        private List<RestaurantPollResult> _pollResults = new List<RestaurantPollResult>();
        private List<RestaurantPollVote> _votes = new List<RestaurantPollVote>();

        public virtual string Name { get; private set; }

        /// <summary>
        /// Data da votação.
        /// </summary>
        public virtual DateTime Date { get; private set; }

        /// <summary>
        /// Todos restaurantes participando da votação.
        /// </summary>
        public virtual IEnumerable<Restaurant> AllRestaurants => _allRestaurants.AsReadOnly();

        /// <summary>
        /// Busca todos os restaurantes válidos para esta votação.
        /// </summary>
        /// <returns>Retorna os restaurantes válidos para receberem votos.</returns>
        public IEnumerable<Restaurant> GetValidRestaurantsForPoll()
        {
            return this.AllRestaurants.Where(restaurant => !DidRestaurantWonThisWeek(restaurant));
        }

        /// <summary>
        /// Contém os ultimos 100 resultados ordenados por Data Decrescente.
        /// </summary>
        public virtual IEnumerable<RestaurantPollResult> PollResults => _pollResults.AsReadOnly();

        /// <summary>
        /// Votos da votação.
        /// </summary>
        public virtual IEnumerable<RestaurantPollVote> Votes => _votes.AsReadOnly();

        /// <summary>
        /// Restaurante vencedor da votação.
        /// </summary>
        public virtual Restaurant WinnerRestaurant { get; private set; }

        public void AddVote(Restaurant restaurant, User user)
        {
            Guard.Against.Null(restaurant, nameof(restaurant));
            Guard.Against.Null(user, nameof(user));

            if (_votes.Any(vote => vote.Voter.Id == user.Id))
            {
                throw new ApplicationException($"User {user.FullName} already voted in this poll.");
            }

            _votes.Add(new RestaurantPollVote(this, user, restaurant, Date));
        }

        /// <summary>
        /// Retorna o voto de um usuário.
        /// </summary>
        /// <param name="user">Usuário que votou.</param>
        /// <returns>Retorna o voto caso o usuário o tenha realizado.</returns>
        public RestaurantPollVote GetUserVote(User user)
        {
            return Votes.FirstOrDefault(vote => vote.Voter.Id == user.Id);
        }

        public bool DidRestaurantWonThisWeek(Restaurant restaurant)
        {
            Guard.Against.Null(restaurant, nameof(restaurant));

            var restaurantLastVictory = _pollResults.FirstOrDefault(result => result.WinnerRestaurant.Id == restaurant.Id);
            return (restaurantLastVictory != null && restaurantLastVictory.Date.WeekOfYear() >= DateTime.Now.WeekOfYear());
        }

        public void SetPollResults(IEnumerable<RestaurantPollResult> pollResults)
        {
            Guard.Against.Null(pollResults, nameof(pollResults));

            if (pollResults.Any(result => result.Date.Date > this.Date.Date))
            {
                throw new InvalidOperationException("poll result cannot have a date greater than this poll date.");
            }

            _pollResults = pollResults.OrderByDescending(result => result.Date).ToList();
        }

        public void SetVotes(IEnumerable<RestaurantPollVote> votes)
        {
            Guard.Against.Null(votes, nameof(votes));

            if (_votes.Any(v => v.RestaurantPoll.Id != this.Id))
            {
                throw new InvalidOperationException("votes do not match with this pollId.");
            }

            //if (_votes.Any(v => v.Date.Date != this.Date.Date))
            //{
            //    throw new InvalidOperationException("votes do not match dates with this poll date.");
            //}

            _votes = votes.ToList();
        }

        public void SetRestaurants(IEnumerable<Restaurant> restaurants)
        {
            _allRestaurants = restaurants.ToList();
        }

        // TODO: Validar se não é melhor colocar este método no Application Service...
        public IEnumerable<RestaurantPollRanking> GetRanking()
        {
            var dictRestaurantes = this.Votes
                .GroupBy(v => v.Restaurant.Id).ToDictionary(g => g.Key, g => g.First().Restaurant);

            return this.Votes.GroupBy(v => v.Restaurant.Id)
                .Select(g => new RestaurantPollRanking(g.Count(), dictRestaurantes[g.Key]))
                .OrderByDescending(it => it.Votes);
        }

        public RestaurantPollResult GetResult()
        {
            if (this.Votes.Count() > 0)
            {
                var mostVoted = this.Votes
                    .GroupBy(v => v.Restaurant.Id)
                    .OrderByDescending(g => g.Count())
                    .First();
                var mostVotedRestaurant = mostVoted.First().Restaurant;
                var votesReceived = mostVoted.Count();

                this.WinnerRestaurant = mostVotedRestaurant;

                var result = new RestaurantPollResult(Guid.NewGuid(), this, Date.Date, mostVotedRestaurant, votesReceived);
                _pollResults.Add(result);

                return result;
            }

            return null;
        }

        #region Constructor

        public RestaurantPoll(Guid id,
            string name,
            DateTime date,
            IEnumerable<Restaurant> restaurants)
        {
            Guard.Against.NullOrEmpty(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            if (date <= DateTime.MinValue) throw new ArgumentNullException(nameof(date));
            Guard.Against.NullOrEmpty(restaurants, nameof(restaurants));

            //if (winnerRestaurant != null && !restaurants.Contains(winnerRestaurant))
            //    throw new InvalidOperationException($"Restaurant '{winnerRestaurant.Name}' is not in the list of participating restaurants.");

            Id = id;
            Name = name;
            Date = date;
            _allRestaurants = restaurants.ToList();
            //WinnerRestaurant = winnerRestaurant;
        }

        // Entity Framework
        protected RestaurantPoll() { }

        #endregion
    }
}
