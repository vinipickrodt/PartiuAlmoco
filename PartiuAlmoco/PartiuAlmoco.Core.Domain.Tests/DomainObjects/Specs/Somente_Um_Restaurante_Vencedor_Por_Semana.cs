using Moq;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using PartiuAlmoco.Core.Domain.Services;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests.DomainObjects.Specs
{
    public class Somente_Um_Restaurante_Vencedor_Por_Semana
    {
        [Fact]
        [Trait("Category", "Specification")]
        public void Specification_Somente_Um_Restaurante_Vencedor_Por_Semana()
        {
            List<Mock<User>> mockedUsers = TestArtifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = TestArtifacts.GetMockedRestaurants();

            var restaurant1 = TestArtifacts.GetRestaurant1();
            var restaurant2 = TestArtifacts.GetRestaurant2();
            var poll = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 9, 13), new List<Restaurant>() { restaurant1, restaurant2 });
            var pollResults = new List<RestaurantPollResult>()
            {
                new RestaurantPollResult(Guid.NewGuid(), poll, new DateTime(2020, 9, 6), restaurant1, 42)
            };
            poll.SetPollResults(pollResults);

            var user = TestArtifacts.GetUser();

            poll.AddVote(restaurant1, user);
            Assert.Single(poll.Votes);

            var poll2 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 9, 12), new List<Restaurant>() { restaurant1, restaurant2 });
            poll2.SetPollResults(pollResults);

            Assert.ThrowsAny<Exception>(() => poll.AddVote(restaurant1, user));
            Assert.Empty(poll2.Votes);
        }

        [Fact]
        [Trait("Category", "Specification")]
        public void Specification_Listagem_Somente_Restaurantes_Que_Nao_Venceram_Na_Semana()
        {
            List<Mock<User>> mockedUsers = TestArtifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = TestArtifacts.GetMockedRestaurants();

            var restaurant1 = TestArtifacts.GetRestaurant1();
            var restaurant2 = TestArtifacts.GetRestaurant2();
            var poll1 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 9, 19), new List<Restaurant>() { restaurant1, restaurant2 });
            var pollResults = new List<RestaurantPollResult>()
            {
                new RestaurantPollResult(Guid.NewGuid(), poll1, new DateTime(2020, 9, 13), restaurant1, 42)
            };
            poll1.SetPollResults(pollResults);

            Assert.Single(poll1.GetValidRestaurantsForPoll());
            Assert.Equal(restaurant2, poll1.GetValidRestaurantsForPoll().First());

            var poll2 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 9, 19), new List<Restaurant>() { restaurant1, restaurant2 });
            var pollResults2 = new List<RestaurantPollResult>()
            {
                new RestaurantPollResult(Guid.NewGuid(), poll2, new DateTime(2020, 9, 12), restaurant1, 42)
            };
            poll2.SetPollResults(pollResults2);

            Assert.Equal(2, poll2.GetValidRestaurantsForPoll().Count());
        }

        [Fact]
        [Trait("Category", "Regressão")]
        public void Mesmo_Se_A_Votacao_Anterior_Nao_Tiver_Sido_Finalizada()
        {
            List<Mock<User>> mockedUsers = TestArtifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = TestArtifacts.GetMockedRestaurants();

            var restaurant1 = TestArtifacts.GetRestaurant1();
            var restaurant2 = TestArtifacts.GetRestaurant2();
            var poll_Dia1 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 1, 10), new List<Restaurant>() { restaurant1, restaurant2 });

            poll_Dia1.AddVote(restaurant1, mockedUsers[0].Object);
            poll_Dia1.AddVote(restaurant1, mockedUsers[1].Object);
            poll_Dia1.AddVote(restaurant1, mockedUsers[2].Object);
            poll_Dia1.AddVote(restaurant2, mockedUsers[3].Object);

            var mockRepo = new Mock<IRestaurantPollRepository>();
            var service = new RestaurantPollService(mockRepo.Object);

            mockRepo.Setup(r => r.GetUnfinishedPolls(It.IsAny<DateTime>())).Returns(new RestaurantPoll[] { poll_Dia1 });
            var dia2 = new DateTime(2020, 1, 11);

            service.GetPollByDate(dia2);

            // ao buscar a votação do dia 11/1/2020 deverá chamar o método FinishPolls
            mockRepo.Verify(r => r.AddPollResult(It.IsAny<RestaurantPollResult>()), Times.Once());
            mockRepo.Verify(r => r.GetPollByDate(It.Is<DateTime>(c => c == dia2)), Times.Once());
        }       
    }
}
