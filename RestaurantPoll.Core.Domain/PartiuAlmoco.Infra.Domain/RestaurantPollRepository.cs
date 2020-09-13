using Ardalis.GuardClauses;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Linq;

namespace PartiuAlmoco.Infra.Domain
{
    public class RestaurantPollRepository : IRestaurantPollRepository
    {
        private PartiuAlmocoDbContext dbContext = new PartiuAlmocoDbContext();

        public RestaurantPoll GetPollByDate(DateTime date)
        {
            if (date <= DateTime.MinValue)
            {
                throw new ArgumentNullException(nameof(date));
            }

            var poll = dbContext.RestaurantPolls
                .FirstOrDefault(poll => poll.Date.Date == date.Date);

            if (poll == null)
            {
                return null;
            }

            return poll;
        }

        public void Confirm(RestaurantPoll poll)
        {
            throw new NotImplementedException();
        }
    }
}
