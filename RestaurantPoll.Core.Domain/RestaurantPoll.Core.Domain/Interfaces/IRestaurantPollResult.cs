using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IRestaurantPollResult
    {
        DateTime Date { get; }
        RestaurantPoll RestaurantPoll { get; }
        int TotalVotes { get; }
        Restaurant WinnerRestaurant { get; }
    }
}