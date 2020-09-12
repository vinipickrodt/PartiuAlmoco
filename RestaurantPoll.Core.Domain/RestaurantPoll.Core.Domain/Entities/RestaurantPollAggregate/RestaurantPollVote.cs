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
    }
}
