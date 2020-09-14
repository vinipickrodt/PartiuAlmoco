using Ardalis.GuardClauses;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Infra.Domain
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public PartiuAlmocoDbContext dbContext = null;

        public RestaurantRepository(PartiuAlmocoDbContext dbContext)
        {
            Guard.Against.Null(dbContext, nameof(dbContext));

            this.dbContext = dbContext;
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return dbContext.Restaurants.Take(1000).ToList();
        }

        public Restaurant GetById(Guid id)
        {
            return dbContext.Restaurants.Find(id);
        }

        public void Add(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();
        }

        public void Update(Restaurant restaurant)
        {
            dbContext.Restaurants.Update(restaurant);
            dbContext.SaveChanges();
        }

        public void Remove(Restaurant restaurant)
        {
            dbContext.Restaurants.Remove(restaurant);
            dbContext.SaveChanges();
        }
    }
}
