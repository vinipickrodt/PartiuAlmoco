//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using PartiuAlmoco.Core.Domain.Entities;
//using PartiuAlmoco.Core.Domain.Interfaces;
//using PartiuAlmoco.Web.Api.DTO;

//namespace PartiuAlmoco.Web.Api.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class GenericController< RepositoryType> : ControllerBase
//    {
//        private readonly IRestaurantRepository repository;
//        private readonly ILogger<RestaurantController> logger;

//        public GenericController(IRestaurantRepository repository, ILogger<RestaurantController> logger)
//        {
//            this.repository = repository;
//            this.logger = logger;
//        }

//        [HttpGet]
//        public IEnumerable<Restaurant> Get()
//        {
//            return repository.GetAllRestaurants();
//        }

//        [HttpPost]
//        [Route("Create")]
//        [Authorize]
//        public RestaurantDTO Create(string name, string website, string phone)
//        {
//            return repository.Add(new Restaurant(Guid.NewGuid(), name, website, phone));
//        }

//        [HttpPost]
//        [Route("Update")]
//        [Authorize]
//        public ActionResult<RestaurantDTO> Update(RestaurantDTO restaurantDTO)
//        {
//            var restaurant = repository.GetById(restaurantDTO.Id);

//            if (restaurant == null)
//            {
//                return NotFound();
//            }

//            // TODO: AutoMapper
//            restaurant.Name = restaurantDTO.Name;
//            restaurant.Website = restaurantDTO.Website;
//            restaurant.Phone = restaurantDTO.Phone;

//            repository.Update(restaurant);

//            return NoContent();
//        }
//    }
//}
