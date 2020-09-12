using System;

namespace RestaurantPoll.Core.Domain.Entities.RestaurantPollAggregate
{
    /// <summary>
    /// Voto em um restaurante feito por um usuário.
    /// </summary>
    public class RestaurantPollVote : Entity
    {
        /// <summary>
        /// Votação que recebeu o voto.
        /// </summary>
        public RestaurantPoll RestaurantPollId { get; protected set; }

        /// <summary>
        /// Usuário que votou.
        /// </summary>
        public User VoterId { get; protected set; }

        /// <summary>
        /// Restaurante que foi votado.
        /// </summary>
        public Restaurant Restaurant { get; protected set; }

        /// <summary>
        /// Data que foi feito o voto.
        /// </summary>
        public DateTime VoteDate { get; protected set; }
    }
}
