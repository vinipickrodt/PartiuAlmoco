using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate
{
    /// <summary>
    /// Resultado da votação.
    /// </summary>
    public class RestaurantPollResult : Entity
    {
        /// <summary>
        /// Votação.
        /// </summary>
        public virtual RestaurantPoll RestaurantPoll { get; private set; }

        /// <summary>
        /// Data do resultado da votação
        /// </summary>
        public virtual DateTime Date { get; private set; }

        /// <summary>
        /// Restaurante que venceu.
        /// </summary>
        public virtual Restaurant WinnerRestaurant { get; private set; }

        /// <summary>
        /// Total de votos da votação.
        /// </summary>
        public virtual int TotalVotes { get; private set; }

        public RestaurantPollResult(Guid id, RestaurantPoll restaurantPoll, DateTime date, Restaurant winnerRestaurant, int totalVotes)
        {
            Guard.Against.NullOrEmpty(id, nameof(id));
            Guard.Against.Null(restaurantPoll, nameof(restaurantPoll));
            Guard.Against.Null(winnerRestaurant, nameof(winnerRestaurant));
            Guard.Against.NegativeOrZero(totalVotes, nameof(totalVotes));

            if (date <= DateTime.MinValue)
            {
                throw new ArgumentException("Invalid date");
            }

            Id = id;
            RestaurantPoll = restaurantPoll;
            Date = date;
            WinnerRestaurant = winnerRestaurant;
            TotalVotes = totalVotes;
        }

        // Entity Framework
        protected RestaurantPollResult() { }
    }
}
