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
    public class RestaurantsController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetRestaurants()
        {
            return Ok(RestaurantsStore.Current.Restaurants);
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurant(int id)
        {
            var res = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
