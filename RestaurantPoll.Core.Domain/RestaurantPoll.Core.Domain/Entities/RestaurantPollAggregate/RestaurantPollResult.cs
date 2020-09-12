using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate
{
    /// <summary>
    /// Resultado da votação.
    /// </summary>
    public class RestaurantPollResult : Entity, IRestaurantPollResult
    {
        /// <summary>
        /// Votação.
        /// </summary>
        public RestaurantPoll RestaurantPoll { get; private set; }

        /// <summary>
        /// Data do resultado da votação
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Restaurante que venceu.
        /// </summary>
        public Restaurant WinnerRestaurant { get; private set; }

        /// <summary>
        /// Total de votos da votação.
        /// </summary>
        public int TotalVotes { get; private set; }
    }
}
