using BestFood.DTOs;
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
        [HttpGet("{restaurantId}/specialties")]
        public IActionResult GetRestaurant(int restaurantId)
        {
            var restaurant = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant.Specialties);
        }

        [HttpGet("{restaurantId}/specialties/{specialtyId}")]
        public IActionResult GetRestaurant(int restaurantId, int specialtyId)
        {
            var restaurant = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            var specialty = restaurant.Specialties.FirstOrDefault(x => x.Id == specialtyId);
            if (specialty == null)
            {
                return NotFound();
            }

            return Ok(specialty);
        }
    }
}
