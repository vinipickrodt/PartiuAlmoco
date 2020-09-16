using AutoMapper;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Web.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartiuAlmoco.Web.Api.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantDTO, Restaurant>();
        }
    }
}
