using Moq;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var mock = new Mock<RestaurantPoll>();
            mock.SetupGet(r => r.Name).Returns("HELLO");
        }
    }
}
