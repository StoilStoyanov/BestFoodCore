using BestFood.DTOs;
using BetFood.Data;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetRestaurant")]
        public IActionResult GetRestaurant(int id)
        {
            var res = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }


        [HttpPost]
        public IActionResult CreateRestaurant([FromBody] RestaurantCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var maxRestaurantId = RestaurantsStore.Current.Restaurants.Max(x => x.Id);
            var newRestaurant = new RestaurantDto()
            {
                Id = ++maxRestaurantId,
                Name = dto.Name
            };

            RestaurantsStore.Current.Restaurants.Add(newRestaurant);
            return CreatedAtRoute("GetRestaurant", new { id = newRestaurant.Id }, newRestaurant);
        }

        [HttpPut]
        public IActionResult UpdateRestaurant([FromBody] RestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var restaurant = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == dto.Id);

            if (restaurant == null)
            {
                return NotFound();
            }

            restaurant.Name = dto.Name;
            restaurant.Specialties = dto.Specialties;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateRestaurant(int id, [FromBody] JsonPatchDocument<RestaurantUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            
            var restaurant = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = new RestaurantUpdateDto()
            {
                Name = restaurant.Name,
                Specialties = restaurant.Specialties
            };

            patchDoc.ApplyTo(restaurantDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            restaurant.Name = restaurantDto.Name;
            restaurant.Specialties = restaurantDto.Specialties;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            var restaurant = RestaurantsStore.Current.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            RestaurantsStore.Current.Restaurants.Remove(restaurant);
            return NoContent();
        }

    }
}
