using Moq;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Tests
{
    public class TestArtifacts
    {
        public static Restaurant GetRestaurant1() => new Restaurant(new Guid("{96E3D259-34E4-4ACC-9C1F-AA47EEB1CB93}"), "Mamma Mia", "http://www.galetomammamia.com.br/", "");
        public static Restaurant GetRestaurant2() => new Restaurant(new Guid("{41055914-E9E6-4308-AF9A-843A65E02CFD}"), "Pizza Hut", "http://www.pizzahut.com.br", "");
        public static Restaurant GetRandomRestaurant() => new Restaurant(Guid.NewGuid(), "Restaurante - " + Guid.NewGuid(), "http://www.site.com.br", "");
        public static RestaurantPoll GetRestaurantPoll() => new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", DateTime.Now, new List<Restaurant>() { GetRestaurant1() });
        public static User GetUser() => new User(new Guid("{84FF7DCB-3A79-4915-9E09-669F09CCB068}"), "John Jones", "john", "john@gmail.com");

        public static List<Mock<Restaurant>> GetMockedRestaurants()
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

        public static List<Mock<User>> GetMockedUsers()
        {
            var userJohn = new Mock<User>(new object[] {
                new Guid("{BE90FABC-C5DE-4778-A37A-A2EC7BBB41BF}"),
                "John Juarez Jones",
                "3j",
                "john@gmail.com"
            });

            var userMary = new Mock<User>(new object[] {
                new Guid("{E8870CB0-B90D-4334-AD7B-13C579A5BBA6}"),
                "Mary Ann Smith",
                "mary",
                "mary@gmail.com"
            });

            var userBill = new Mock<User>(new object[] {
                new Guid("{BBCF1FB7-6536-4639-B7CE-0CE62FED8419}"),
                "Bill Highsmith",
                "billy",
                "billy@gmail.com"
            });

            var userCaroline = new Mock<User>(new object[] {
                new Guid("{8C709D68-8EAB-4A95-8ACA-8FCA330F018B}"),
                "Caroline Gracie Wurst",
                "carol",
                "carol@hotmail.com"
            });

            return new List<Mock<User>>() { userJohn, userMary, userBill, userCaroline };
        }
    }
}
