using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IRestaurantPoll
    {
        string Name { get; }
        DateTime Date { get; }
        IEnumerable<Restaurant> AllRestaurants { get; }
        IEnumerable<RestaurantPollResult> PollResultsFromLast30Days { get; }
        IEnumerable<RestaurantPollVote> Votes { get; }
        Restaurant WinnerRestaurant { get; }
    }
}
