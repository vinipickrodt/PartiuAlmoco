using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IRestaurantPollService
    {
        RestaurantPoll GetPollByDate(DateTime date);
        RestaurantPoll GetTodayRestaurantPoll();
        void ProcessUnfinishedPolls(DateTime date);
    }
}
