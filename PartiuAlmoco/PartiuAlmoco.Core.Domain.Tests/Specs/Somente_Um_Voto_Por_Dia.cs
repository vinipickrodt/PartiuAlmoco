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
            List<Mock<User>> mockedUsers = TestArtifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = TestArtifacts.GetMockedRestaurants();

            var restaurantPoll = TestArtifacts.GetRestaurantPoll();
            var restaurant = TestArtifacts.GetRestaurant1();
            var user = TestArtifacts.GetUser();

            restaurantPoll.AddVote(restaurant, user);
            Assert.Single(restaurantPoll.Votes);

            Assert.ThrowsAny<Exception>(() => restaurantPoll.AddVote(restaurant, user));
            Assert.Single(restaurantPoll.Votes);
        }
    }
}
