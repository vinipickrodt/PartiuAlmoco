﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PartiuAlmoco.Core.Domain.Entities;
using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartiuAlmoco.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantPollController : ControllerBase
    {
        private readonly IRestaurantPollRepository repository;
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<RestaurantPollController> logger;

        public RestaurantPollController(IRestaurantPollRepository repository, IRestaurantRepository restaurantRepository, IUserRepository userRepository, ILogger<RestaurantPollController> logger)
        {
            this.repository = repository;
            this.restaurantRepository = restaurantRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Busca os restaurantes que podem receber votos para na votação de hoje.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route(nameof(GetRestaurantsValidForPoll))]
        public ActionResult<IEnumerable<Restaurant>> GetRestaurantsValidForPoll()
        {
            try
            {
                RestaurantPoll poll = GetDefaultPoll();
                return Ok(poll.GetValidRestaurantsForPoll());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Vote))]
        public ActionResult Vote(Guid restaurantId)
        {
            try
            {
                var poll = GetDefaultPoll();
                var restaurant = restaurantRepository.GetById(restaurantId);
                poll.AddVote(restaurant, GetCurrentUser());
                repository.Confirm(poll);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(GetMyVote))]
        public ActionResult GetMyVote()
        {
            try
            {
                var vote = GetDefaultPoll().GetUserVote(GetCurrentUser())?.Restaurant?.Id;
                return Ok(vote.HasValue ? (object)vote.Value : null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route(nameof(GetRanking))]
        public IActionResult GetRanking()
        {
            try
            {
                return Ok(GetDefaultPoll().GetRanking());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private User GetCurrentUser()
        {
            var id = this.User.FindFirst("id")?.Value;
            if (string.IsNullOrWhiteSpace(id))
                throw new ApplicationException("Invalid user");
            return userRepository.GetById(new Guid(id));
        }

        private RestaurantPoll GetDefaultPoll()
        {
            var poll = repository.GetPollByDate(DateTime.Now);

            if (poll == null)
            {
                poll = repository.NewPoll("Almoço", DateTime.Now);
            }

            return poll;
        }
    }
}
