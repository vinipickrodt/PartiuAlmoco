using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate
{
    public class RestaurantPoll : Entity, IRestaurantPoll, IAggregateRoot
    {
        public string Name { get; private set; }

        /// <summary>
        /// Data da votação.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Todos restaurantes participando da votação.
        /// </summary>
        public IEnumerable<Restaurant> AllRestaurants { get; private set; }

        /// <summary>
        /// Contém os ultimos 30 resultados ordenados por Data Decrescente.
        /// </summary>
        public IEnumerable<RestaurantPollResult> PollResultsFromLast30Days { get; private set; }

        /// <summary>
        /// Votos da votação.
        /// </summary>
        public IEnumerable<RestaurantPollVote> Votes { get; private set; }

        /// <summary>
        /// Restaurante vencedor da votação.
        /// </summary>
        public Restaurant WinnerRestaurant { get; private set; }

        public void AddVote(Restaurant restaurant, User user)
        {
            // TODO: regras de negocio
        }
    }
}
