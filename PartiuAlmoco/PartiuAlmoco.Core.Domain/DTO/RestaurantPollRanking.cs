using PartiuAlmoco.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.DTO
{
    public class RestaurantPollRanking
    {
        public int Votes { get; set; }
        public Restaurant Restaurant { get; set; }

        public RestaurantPollRanking(int votes, Restaurant restaurant)
        {
            Votes = votes;
            Restaurant = restaurant;
        }
    }
}
