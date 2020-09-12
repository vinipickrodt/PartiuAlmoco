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
    }
}
