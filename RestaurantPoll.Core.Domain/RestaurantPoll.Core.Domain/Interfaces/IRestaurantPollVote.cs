using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IRestaurantPollVote
    {
        RestaurantPoll RestaurantPoll { get; }
        DateTime Date { get; }
        Restaurant Restaurant { get; }
        User Voter { get; }
    }
}
