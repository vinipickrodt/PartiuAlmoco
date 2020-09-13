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
    public class RestaurantPollResult_Construction
    {
        [Theory]
        [InlineData("{E9F79E7E-B18B-457D-AAC7-0E091A388BD5}")]
        public void RestaurantPollResult_Pass_Valid_Id(string guidStr)
        {
            var validGuid = new Guid(guidStr);
            new RestaurantPollResult(validGuid, Artifacts.GetRestaurantPoll(), DateTime.Now, Artifacts.GetRestaurant1(), 42);
        }

        [Theory]
        [InlineData("{00000000-0000-0000-0000-000000000000}")]
        public void RestaurantPollResult_Fails_Invalid_Id(string guidStr)
        {
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPollResult(new Guid(guidStr), Artifacts.GetRestaurantPoll(), DateTime.Now, Artifacts.GetRestaurant1(), 42)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5456465)]
        [InlineData(-9999999)]
        public void RestaurantPollResult_Fails_Invalid_Votes(int votes)
        {
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPollResult(Guid.NewGuid(), Artifacts.GetRestaurantPoll(), DateTime.Now, Artifacts.GetRestaurant1(), votes)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(5456465)]
        [InlineData(9999999)]
        public void RestaurantPollResult_Pass_Valid_Votes(int votes)
        {
            new RestaurantPollResult(Guid.NewGuid(), Artifacts.GetRestaurantPoll(), DateTime.Now, Artifacts.GetRestaurant1(), votes);
        }

        [Fact]
        public void RestaurantPollResult_Has_Valid_Winner()
        {
            var validGuid = Guid.NewGuid();
            var restaurant = Artifacts.GetRestaurant1();
            var restaurantPoll = Artifacts.GetRestaurantPoll();

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPollResult(validGuid, restaurantPoll, DateTime.Now, null, 42)));

            // pass
            new RestaurantPollResult(validGuid, restaurantPoll, DateTime.Now, restaurant, 42);
        }

        [Theory]
        [InlineData(12, 2, 1988)]
        public void RestaurantPollResult_Pass_Valid_Date(int dia, int mes, int ano)
        {
            var validGuid = Guid.NewGuid();
            var restaurant = Artifacts.GetRestaurant1();
            var restaurantPoll = Artifacts.GetRestaurantPoll();

            new RestaurantPollResult(validGuid, restaurantPoll, new DateTime(ano, mes, dia), restaurant, 42);
        }

        [Fact]
        public void RestaurantPollResult_Fails_Invalid_Date()
        {
            var validGuid = Guid.NewGuid();
            var restaurant = Artifacts.GetRestaurant1();
            var restaurantPoll = Artifacts.GetRestaurantPoll();

            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPollResult(validGuid, restaurantPoll, default, restaurant, 42)));
        }

        [Fact]
        public void RestaurantPollResult_Has_Valid_RestaurantPoll()
        {
            var validGuid = Guid.NewGuid();
            var restaurant = Artifacts.GetRestaurant1();
            var restaurantPoll = Artifacts.GetRestaurantPoll();

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() => new RestaurantPollResult(validGuid, null, DateTime.Now, restaurant, 42)));

            // pass
            new RestaurantPollResult(validGuid, restaurantPoll, DateTime.Now, restaurant, 42);
        }
    }
}
