using Moq;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests
{
    public class Specs
    {
        [Fact]
        public void Specification_Somente_Um_Voto_Por_Dia()
        {
            List<Mock<Entities.User>> mockedUsers = GetMockedUsers();
            List<Mock<Restaurant>> mockedRestaurants = GetMockedRestaurants();
        }

        private List<Mock<Restaurant>> GetMockedRestaurants()
        {
            var restaurantPizzaria = new Mock<Restaurant>(new object[] { "Pizza Hut", "http://www.pizzahut.com.br", "" });
            var restaurantChurrascaria = new Mock<Restaurant>(new object[] { "El Fuego", "", "(51) 3346-1773" });
            var restaurantGaleteria= new Mock<Restaurant>(new object[] { "Mamma Mia", "http://www.galetomammamia.com.br/", "" });
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
