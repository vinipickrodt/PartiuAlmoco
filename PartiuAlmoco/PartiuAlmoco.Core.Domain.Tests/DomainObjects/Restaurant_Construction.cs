using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests.DomainObjects
{
    public class Restaurant_Construction
    {
        [Fact]
        public void Restaurants_Has_Valid_Id()
        {
            var validGuid = Guid.NewGuid();
            var restaurants = new List<Restaurant>();
            restaurants.Add(new Restaurant(validGuid, "Pizza Hut", "http://www.pizzahut.com", ""));

            // must fail
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(Guid.Empty, "Nome", "http://www.test.com", "")));
        }

        [Fact]
        public void Restaurants_Has_Valid_Name()
        {
            var validGuid = Guid.NewGuid();

            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, null, "http://www.site.com", "(51)98765-4321")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "", "http://www.site.com", "(51)98765-4321")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "     ", "http://www.site.com", "(51)98765-4321")));
        }

        [Fact]
        public void Restaurants_Has_Website_Or_Phone()
        {
            var validGuid = Guid.NewGuid();

            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "", "")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "", null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", null, "")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", null, null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "   ", null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "   ", "    ")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", null, "    ")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "    ", null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "    ", "")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Restaurant(validGuid, "Nome do Restaurante", "", "    ")));
        }
    }
}
