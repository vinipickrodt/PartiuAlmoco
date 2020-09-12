using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate
{
    public class RestaurantPoll : Entity, IAggregateRoot
    {
        public virtual string Name { get; private set; }

        /// <summary>
        /// Data da votação.
        /// </summary>
        public virtual DateTime Date { get; private set; }

        /// <summary>
        /// Todos restaurantes participando da votação.
        /// </summary>
        public virtual IEnumerable<Restaurant> AllRestaurants { get; private set; }

        /// <summary>
        /// Contém os ultimos 30 resultados ordenados por Data Decrescente.
        /// </summary>
        public virtual IEnumerable<RestaurantPollResult> PollResultsFromLast30Days { get; private set; }

        /// <summary>
        /// Votos da votação.
        /// </summary>
        public virtual IEnumerable<RestaurantPollVote> Votes { get; private set; }

        /// <summary>
        /// Restaurante vencedor da votação.
        /// </summary>
        public virtual Restaurant WinnerRestaurant { get; private set; }

        public void AddVote(Restaurant restaurant, User user)
        {
            // TODO: regras de negocio
        }
    }
}
