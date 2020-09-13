using Ardalis.GuardClauses;
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
        private List<RestaurantPollResult> _pollResultsFromLast30Days = new List<RestaurantPollResult>();
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
        /// Contém os ultimos 30 resultados ordenados por Data Decrescente.
        /// </summary>
        public virtual IEnumerable<RestaurantPollResult> PollResultsFromLast30Days => _pollResultsFromLast30Days.AsReadOnly();

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
                throw new ApplicationException($"User ${user.FullName} already voted in this poll.");
            }

            _votes.Add(new RestaurantPollVote(this, user, restaurant, DateTime.Now));
        }

        public bool DidRestaurantWonThisWeek(Restaurant restaurant)
        {
            Guard.Against.Null(restaurant, nameof(restaurant));

            var restaurantLastVictory = _pollResultsFromLast30Days.FirstOrDefault(result => result.WinnerRestaurant.Id == restaurant.Id);
            return (restaurantLastVictory != null && restaurantLastVictory.Date.WeekOfYear() >= DateTime.Now.WeekOfYear());
        }

        #region Constructor

        public RestaurantPoll(Guid id,
            string name,
            DateTime date,
            IEnumerable<Restaurant> restaurants,
            IEnumerable<RestaurantPollResult> pollResultsFromLast30Days,
            IEnumerable<RestaurantPollVote> votes,
            Restaurant winnerRestaurant)
        {
            Guard.Against.NullOrEmpty(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            if (date <= DateTime.MinValue) throw new ArgumentNullException(nameof(date));
            Guard.Against.NullOrEmpty(restaurants, nameof(restaurants));
            Guard.Against.Null(pollResultsFromLast30Days, nameof(pollResultsFromLast30Days));
            Guard.Against.Null(votes, nameof(votes));

            if (winnerRestaurant != null && !restaurants.Contains(winnerRestaurant))
                throw new InvalidOperationException($"Restaurant '{winnerRestaurant.Name}' is not in the list of participating restaurants.");

            Id = id;
            Name = name;
            Date = date;
            _allRestaurants = restaurants.ToList();
            _pollResultsFromLast30Days = pollResultsFromLast30Days.ToList();
            _votes = votes.ToList();
            WinnerRestaurant = winnerRestaurant;
        }

        #endregion
    }
}
