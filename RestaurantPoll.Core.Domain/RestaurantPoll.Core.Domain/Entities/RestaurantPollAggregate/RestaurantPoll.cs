using RestaurantPoll.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantPoll.Core.Domain.Entities.RestaurantPollAggregate
{
    public class RestaurantPoll : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        /// <summary>
        /// Data da votação.
        /// </summary>
        public DateTimeOffset Date { get; private set; }

        /// <summary>
        /// Restaurante vencedor da votação.
        /// </summary>
        public Restaurant WinnerRestaurant { get; private set; }
    }
}
