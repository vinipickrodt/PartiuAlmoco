using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;

namespace PartiuAlmoco.Core.Domain.Tests
{
    public class Somente_Um_Voto_Por_Dia
    {
        [Fact]
        public void Specification_Somente_Um_Voto_Por_Dia()
        {
            List<Mock<User>> mockedUsers = Artifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = Artifacts.GetMockedRestaurants();

            var restaurantPoll = Artifacts.GetRestaurantPoll();
            var restaurant = Artifacts.GetRestaurant1();
            var user = Artifacts.GetUser();

            restaurantPoll.AddVote(restaurant, user);
            Assert.Single(restaurantPoll.Votes);

            Assert.ThrowsAny<Exception>(() => restaurantPoll.AddVote(restaurant, user));
            Assert.Single(restaurantPoll.Votes);
        }
    }
}
