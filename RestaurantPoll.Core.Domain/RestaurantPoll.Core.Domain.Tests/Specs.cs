using Moq;
using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;

namespace PartiuAlmoco.Core.Domain.Tests
{
    public class Specs
    {
        [Fact]
        public void Specification_Somente_Um_Voto_Por_Dia()
        {
            List<Mock<User>> mockedUsers = GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = GetMockedRestaurants();

            var restaurantPoll = Artifacts.GetRestaurantPoll();
            var restaurant = Artifacts.GetRestaurant1();
            var user = Artifacts.GetUser();

            restaurantPoll.AddVote(restaurant, user);
            Assert.Single(restaurantPoll.Votes);

            Assert.ThrowsAny<Exception>(() => restaurantPoll.AddVote(restaurant, user));
            Assert.Single(restaurantPoll.Votes);
        }

        [Fact]
        public void Specification_Somente_Um_Restaurante_Vencedor_Por_Semana()
        {
            List<Mock<User>> mockedUsers = GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = GetMockedRestaurants();

            var restaurant1 = Artifacts.GetRestaurant1();
            var restaurant2 = Artifacts.GetRestaurant2();
            var poll = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 9, 13), new List<Restaurant>() { restaurant1, restaurant2 }, null);
            var pollResults = new List<RestaurantPollResult>()
            {
                new RestaurantPollResult(Guid.NewGuid(), poll, new DateTime(2020, 9, 6), restaurant1, 42)
            };
            poll.SetPollResults(pollResults);

            var user = Artifacts.GetUser();

            poll.AddVote(restaurant1, user);
            Assert.Single(poll.Votes);

            var poll2 = new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", new DateTime(2020, 9, 12), new List<Restaurant>() { restaurant1, restaurant2 }, null);
            poll2.SetPollResults(pollResults);

            Assert.ThrowsAny<Exception>(() => poll.AddVote(restaurant1, user));
            Assert.Empty(poll2.Votes);
        }

        private List<Mock<Restaurant>> GetMockedRestaurants()
        {
            var restaurantPizzaria = new Mock<Restaurant>(new object[] { "Pizza Hut", "http://www.pizzahut.com.br", "" });
            var restaurantChurrascaria = new Mock<Restaurant>(new object[] { "El Fuego", "", "(51) 3346-1773" });
            var restaurantGaleteria = new Mock<Restaurant>(new object[] { "Mamma Mia", "http://www.galetomammamia.com.br/", "" });
            var restaurantBuffet = new Mock<Restaurant>(new object[] { "Delícia Natural", "https://www.facebook.com/DeliciaNaturalRestaurante/", "" });
            var restaurantMassas = new Mock<Restaurant>(new object[] { "Usina de Massas", "https://usinademassas.com.br/", "" });

            return new List<Mock<Restaurant>>()
            {
                restaurantPizzaria,
                restaurantChurrascaria,
                restaurantGaleteria,
                restaurantBuffet,
                restaurantMassas
            };
        }

        private List<Mock<Entities.User>> GetMockedUsers()
        {
            var userJohn = new Mock<Entities.User>(new object[] {
                new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                "John Juarez Jones",
                "3j",
                "john@gmail.com"
            });

            var userMary = new Mock<Entities.User>(new object[] {
                new Guid("{E8870CB0-B90D-4334-AD7B-13C579A5BBA6}"),
                "Mary Ann Smith",
                "mary",
                "mary@gmail.com"
            });

            var userBill = new Mock<Entities.User>(new object[] {
                new Guid("{BBCF1FB7-6536-4639-B7CE-0CE62FED8419}"),
                "Bill Highsmith",
                "billy",
                "billy@gmail.com"
            });

            var userCaroline = new Mock<Entities.User>(new object[] {
                new Guid("{8C709D68-8EAB-4A95-8ACA-8FCA330F018B}"),
                "Caroline Gracie Wurst",
                "carol",
                "carol@hotmail.com"
            });

            return new List<Mock<Entities.User>>() { userJohn, userMary, userBill, userCaroline };
        }
    }
}
