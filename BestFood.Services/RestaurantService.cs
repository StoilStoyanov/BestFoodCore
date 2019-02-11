using BestFood.DTOs;
using BestFood.Entities;
using BestFood.Services.Interfaces;
using BetFood.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace BestFood.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly BestFoodContext _ctx;

        public RestaurantService(BestFoodContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Restaurant> GetAll()
        {
            return _ctx.Restaurants.AsNoTracking().Include(x => x.Specialties).AsQueryable();
        }

        public Restaurant GetById(int id)
        {
            return _ctx.Restaurants.AsNoTracking().Include(x => x.Specialties).FirstOrDefault(x => x.Id == id);
        }

        public Restaurant AddNew(RestaurantCreateDto dto)
        {
            var newRestaurant = new Restaurant()
            {
                Name = dto.Name
            };
            _ctx.Restaurants.Add(newRestaurant);
            _ctx.SaveChanges();
            return newRestaurant;
        }

        public bool Update(RestaurantDto dto)
        {
            var restaurant = _ctx.Restaurants.FirstOrDefault(x => x.Id == dto.Id);
            if (restaurant == null)
            {
                return false;
            }

            restaurant.Name = dto.Name;
            restaurant.Specialties = dto.Specialties.Select(x => new Specialty()
            {
                Name = x.Name
            }).ToList();
            _ctx.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var restaurant = _ctx.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                return false;
            }

            _ctx.Restaurants.Remove(restaurant);
            _ctx.SaveChanges();
            return true;
        }
    }
}
