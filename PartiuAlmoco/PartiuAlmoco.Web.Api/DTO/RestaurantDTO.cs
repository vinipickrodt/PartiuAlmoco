using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartiuAlmoco.Web.Api.DTO
{
    public class RestaurantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
    }
}
