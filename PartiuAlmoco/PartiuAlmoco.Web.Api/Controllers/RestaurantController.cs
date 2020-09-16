using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Interfaces;
using PartiuAlmoco.Web.Api.DTO;
using PartiuAlmoco.Web.Api.Helpers;

namespace PartiuAlmoco.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository repository;
        private readonly ILogger<RestaurantController> logger;

        public RestaurantController(IRestaurantRepository repository, ILogger<RestaurantController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> Get()
        {
            try
            {
                return Ok(repository.GetAllRestaurants());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public ActionResult<RestaurantDTO> Create(RestaurantDTO restaurantDTO)
        {
            try
            {
                if (restaurantDTO == null)
                {
                    return BadRequest("Empty object.");
                }

                restaurantDTO.Id = Guid.NewGuid();
                var restaurant = AutoMappers.Mapper.Map<Restaurant>(restaurantDTO);
                repository.Add(restaurant);

                return CreatedAtAction(nameof(Create), restaurant);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public ActionResult<RestaurantDTO> Update(RestaurantDTO restaurantDTO)
        {
            try
            {
                var restaurant = AutoMappers.Mapper.Map<Restaurant>(restaurantDTO);

                repository.Update(restaurant);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public ActionResult<RestaurantDTO> Delete(RestaurantDTO restaurantDTO)
        {
            try
            {
                var restaurant = repository.GetById(restaurantDTO.Id);

                if (restaurant == null)
                {
                    return NotFound();
                }

                repository.Remove(restaurant);

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(GetById))]
        [Authorize]
        public ActionResult<RestaurantDTO> GetById(Guid id)
        {
            try
            {
                return Ok(repository.GetById(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
