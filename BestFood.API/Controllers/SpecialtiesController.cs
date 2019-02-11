using BestFood.DTOs;
using BestFood.Services.Interfaces;
using BetFood.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFood.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public SpecialtiesController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{restaurantId}/specialties")]
        public IActionResult GetRestaurant(int restaurantId)
        {
            var restaurant = _restaurantService.GetById(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant.Specialties);
        }
    }
}
