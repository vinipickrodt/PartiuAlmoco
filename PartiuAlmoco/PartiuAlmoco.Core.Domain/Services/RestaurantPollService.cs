using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Services
{
    public class RestaurantPollService : IRestaurantPollService
    {
        private readonly IRestaurantPollRepository repository;

        public RestaurantPollService(IRestaurantPollRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public RestaurantPoll GetPollByDate(DateTime date)
        {
            return _GetPollByDate(date, false);
        }

        private RestaurantPoll _GetPollByDate(DateTime date, bool escapeUnfinishedPollsProcessing)
        {
            // this is needed because past results will affect the restaurants who cannot participate in this poll.
            ProcessUnfinishedPolls(date);

            // Ok, now that everything is alright. Just retrieve the poll and return.
            return this.repository.GetPollByDate(date);
        }

        public void ProcessUnfinishedPolls(DateTime date)
        {
            IEnumerable<RestaurantPoll> unfinishedPolls = repository.GetUnfinishedPolls(date);

            if (unfinishedPolls.Count() > 0)
            {
                foreach (var poll in unfinishedPolls)
                {
                    var result = poll.GetResult();
                    repository.AddPollResult(result);
                }
            }
        }

        private static readonly object locker = new object();

        public RestaurantPoll GetTodayRestaurantPoll()
        {
            lock (locker)
            {
                var poll = GetPollByDate(DateTime.Now);

                if (poll == null)
                {
                    poll = repository.NewPoll("Almoço", DateTime.Now);
                }

                return poll;
            }
        }
    }
}