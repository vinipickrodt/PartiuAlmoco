using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PartiuAlmoco.Core.Domain.Tests.DomainObjects
{
    public class Restaurant
    {
        [Fact]
        public void Restaurants_Must_Have_Valid_Name()
        {
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant(null, "http://www.site.com", "(51)98765-4321")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("", "http://www.site.com", "(51)98765-4321")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("     ", "http://www.site.com", "(51)98765-4321")));
        }

        [Fact]
        public void Restaurants_Must_Have_A_Website_Or_Phone()
        {
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "", "")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "", null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", null, "")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", null, null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "   ", null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "   ", "    ")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", null, "    ")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "    ", null)));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "    ", "")));
            Assert.ThrowsAny<ArgumentException>(new Action(() => new Entities.Restaurant("Nome do Restaurante", "", "    ")));
        }
    }
}
