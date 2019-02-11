using System.Linq;
using BestFood.DTOs;
using BestFood.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace BestFood.Services.Interfaces
{
    public interface IRestaurantService
    {
        IQueryable<Restaurant> GetAll();
        Restaurant GetById(int id);
        Restaurant AddNew(RestaurantCreateDto restaurant);
        bool Update(RestaurantDto dto);
        bool Delete(int id);
    }
}