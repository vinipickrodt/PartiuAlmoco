using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Tests
{
    public class Artifacts
    {
        public static Restaurant GetRestaurant1() => new Restaurant(new Guid("{96E3D259-34E4-4ACC-9C1F-AA47EEB1CB93}"), "Mamma Mia", "http://www.galetomammamia.com.br/", "");
        public static Restaurant GetRestaurant2() => new Restaurant(new Guid("{41055914-E9E6-4308-AF9A-843A65E02CFD}"), "Pizza Hut", "http://www.pizzahut.com.br", "");
        public static Restaurant GetRandomRestaurant() => new Restaurant(Guid.NewGuid(), "Restaurante - " + Guid.NewGuid(), "http://www.site.com.br", "");
        public static RestaurantPoll GetRestaurantPoll() => new RestaurantPoll(new Guid("{D6F9DD48-8F80-4B71-8D34-937526BAC306}"), "Almoço", DateTime.Now, new List<Restaurant>() { GetRestaurant1() }, null);
        public static User GetUser() => new User(new Guid("{84FF7DCB-3A79-4915-9E09-669F09CCB068}"), "John Jones", "john", "john@gmail.com");
    }
}
