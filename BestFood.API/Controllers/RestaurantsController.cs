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
        public JsonResult GetRestaurants()
        {
            return new JsonResult(RestaurantsStore.Current.Restaurants);
        }
    }
}
