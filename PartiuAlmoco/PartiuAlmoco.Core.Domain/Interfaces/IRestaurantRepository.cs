using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetAllRestaurants();
        Restaurant GetById(Guid id);
        void Add(Restaurant restaurant);
        void Update(Restaurant restaurant);
        void Remove(Restaurant restaurant);
    }
}
