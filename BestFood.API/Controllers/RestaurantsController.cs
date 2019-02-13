using BestFood.DTOs;
using BestFood.Entities;
using BestFood.Services.Interfaces;
using BetFood.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BestFood.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController: ControllerBase
    {
        private readonly ILogger<RestaurantsController> _logger;
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantService restaurantService)
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Restaurant>), 200)]
        public IActionResult GetRestaurants()
        {
            return Ok(_restaurantService.GetAll());
        }

        [HttpGet("{id}", Name = "GetRestaurant")]
        public IActionResult GetRestaurant(int id)
        {
            var res = _restaurantService.GetById(id);
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

            var newRestaurant = _restaurantService.AddNew(dto);
            return CreatedAtRoute("GetRestaurant", new { id = newRestaurant.Id }, newRestaurant);
        }

        [HttpPut]
        public IActionResult UpdateRestaurant([FromBody] RestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = _restaurantService.Update(dto);
            if(!success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateRestaurant(int id, [FromBody] JsonPatchDocument<RestaurantUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var restaurant = _restaurantService.GetById(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var updateDto = new RestaurantUpdateDto()
            {
                Name = restaurant.Name,
                Specialties = restaurant.Specialties.Select(x => new SpecialtyDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList()
            };
            patchDoc.ApplyTo(updateDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = new RestaurantDto()
            {
                Name = updateDto.Name,
                Specialties = updateDto.Specialties
            };
            var result = _restaurantService.Update(dto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant([Required]int id)
        {
            var success = _restaurantService.Delete(id);
            if (!success)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
