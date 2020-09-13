using Ardalis.GuardClauses;
using System;

namespace PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate
{
    /// <summary>
    /// Voto em um restaurante feito por um usuário.
    /// </summary>
    public class RestaurantPollVote : Entity
    {
        /// <summary>
        /// Votação que recebeu o voto.
        /// </summary>
        public virtual RestaurantPoll RestaurantPoll { get; private set; }

        /// <summary>
        /// Usuário que votou.
        /// </summary>
        public virtual User Voter { get; private set; }

        /// <summary>
        /// Restaurante que foi votado.
        /// </summary>
        public virtual Restaurant Restaurant { get; private set; }

        /// <summary>
        /// Data que foi feito o voto.
        /// </summary>
        public virtual DateTime Date { get; private set; }

        public RestaurantPollVote(RestaurantPoll restaurantPoll, User voter, Restaurant restaurant, DateTime date)
        {
            Guard.Against.Null(restaurantPoll, nameof(restaurantPoll));
            Guard.Against.Null(voter, nameof(voter));
            Guard.Against.Null(restaurant, nameof(restaurant));
            if (date <= DateTime.MinValue)
            {
                throw new ArgumentNullException(nameof(date));
            }

            RestaurantPoll = restaurantPoll;
            Restaurant = restaurant;
            Voter = voter;
            Date = date;
        }

        // Entity Framework
        protected RestaurantPollVote() { }
    }
}
