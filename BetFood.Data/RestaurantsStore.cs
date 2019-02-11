﻿using BestFood.DTOs;
using System;
using System.Collections.Generic;

namespace BetFood.Data
{
    public class RestaurantsStore
    {
        public static RestaurantsStore Current { get; } = new RestaurantsStore();

        public List<RestaurantDto> Restaurants { get; set; }

        public RestaurantsStore()
        {
            this.Restaurants = new List<RestaurantDto>()
            {
                new RestaurantDto { Id=1, Name="65 Fireflies"},
                new RestaurantDto { Id=2, Name="Rocket"}
            };
        }
    }
}
