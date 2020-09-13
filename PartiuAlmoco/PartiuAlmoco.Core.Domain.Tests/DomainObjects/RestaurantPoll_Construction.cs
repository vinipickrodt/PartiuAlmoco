using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests.DomainObjects
{
    public class RestaurantPoll_Construction
    {
        [Fact]
        public void RestaurantPoll_Has_Valid_Id()
        {
            var validGuid = Guid.NewGuid();
            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant(validGuid, "Pizza Hut", "http://www.pizzahut.com", ""));

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() =>
                new RestaurantPoll(Guid.Empty, "Nome", DateTime.Now, restaurants, null)));
        }

        [Fact]
        public void RestaurantPoll_Has_Valid_Name()
        {
            var validGuid = Guid.NewGuid();

            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant(validGuid, "Pizza Hut", "http://www.pizzahut.com", ""));

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPoll(validGuid, null, DateTime.Now, restaurants, null)));

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPoll(validGuid, "", DateTime.Now, restaurants, null)));

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPoll(validGuid, "     ", DateTime.Now, restaurants, null)));

            // must pass
            new RestaurantPoll(validGuid, "Almoço", DateTime.Now, restaurants, null);
        }
    }
}
