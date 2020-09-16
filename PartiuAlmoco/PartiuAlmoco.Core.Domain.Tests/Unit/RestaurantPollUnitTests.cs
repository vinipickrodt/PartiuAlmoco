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
namespace PartiuAlmoco.Core.Domain.Tests.DomainObjects.Unit
{
    public class RestaurantPollUnitTests
    {
        [Fact]
        public void Verifica_Resultado_Da_Votacao()
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

            var result = poll_Dia1.GetResult();

            Assert.Equal(3, result.TotalVotes);
            Assert.Equal(restaurant1, result.WinnerRestaurant);
            Assert.Equal(restaurant1, poll_Dia1.WinnerRestaurant);
        }

        /// <summary>
        /// Pode ocorrer de uma votação anterior não ter sido finalizada. 
        /// O sistema deve finaliza a votação anterior e somente depois disso 
        /// realizar a busca da votação que foi solicitada.
        /// </summary>
        [Fact]
        public void Finaliza_As_Votacoes_Anteriores_Caso_Nao_Tiverem_Sido_Finalizadas()
        {
            List<Mock<User>> mockedUsers = TestArtifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = TestArtifacts.GetMockedRestaurants();

            var restaurant1 = TestArtifacts.GetRestaurant1();
            var restaurant2 = TestArtifacts.GetRestaurant2();
            
            var dia1 = new DateTime(2020, 1, 10);
            var dia2 = new DateTime(2020, 1, 11);
            
            var poll_Dia1 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", dia1, new List<Restaurant>() { restaurant1, restaurant2 });

            poll_Dia1.AddVote(restaurant1, mockedUsers[0].Object);
            poll_Dia1.AddVote(restaurant1, mockedUsers[1].Object);
            poll_Dia1.AddVote(restaurant1, mockedUsers[2].Object);
            poll_Dia1.AddVote(restaurant2, mockedUsers[3].Object);

            var mockRepo = new Mock<IRestaurantPollRepository>();
            var service = new RestaurantPollService(mockRepo.Object);

            mockRepo.Setup(r => r.GetUnfinishedPolls(It.IsAny<DateTime>())).Returns(new RestaurantPoll[] { poll_Dia1 });

            service.GetPollByDate(dia2);

            // ao buscar a votação do dia 11/1/2020 deverá chamar o método AddPollResult
            mockRepo.Verify(r => r.AddPollResult(It.IsAny<RestaurantPollResult>()), Times.Once());
            mockRepo.Verify(r => r.GetPollByDate(It.Is<DateTime>(c => c == dia2)), Times.Once());
        }

        [Fact]
        public void Verifica_Ranking()
        {
            List<Mock<User>> mockedUsers = TestArtifacts.GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = TestArtifacts.GetMockedRestaurants();

            var restaurant1 = TestArtifacts.GetRestaurant1();
            var restaurant2 = TestArtifacts.GetRestaurant2();
            var poll_Dia1 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 1, 10), new List<Restaurant>() { restaurant1, restaurant2 });

            poll_Dia1.AddVote(restaurant1, mockedUsers[0].Object);
            poll_Dia1.AddVote(restaurant1, mockedUsers[1].Object);
            poll_Dia1.AddVote(restaurant2, mockedUsers[2].Object);

            var ranking = poll_Dia1.GetRanking();

            Assert.Equal(2, ranking.ElementAt(0).Votes);
            Assert.Equal(1, ranking.ElementAt(1).Votes);
            Assert.Equal(restaurant1, ranking.ElementAt(0).Restaurant);
            Assert.Equal(restaurant2, ranking.ElementAt(1).Restaurant);
        }
    }
}
