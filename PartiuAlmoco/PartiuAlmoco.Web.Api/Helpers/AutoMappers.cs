using AutoMapper;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Web.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartiuAlmoco.Web.Api.Helpers
{
    public static class AutoMappers
    {
        public static IMapper Mapper = null;

        static AutoMappers()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantDTO>();
                cfg.CreateMap<RestaurantDTO, Restaurant>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
