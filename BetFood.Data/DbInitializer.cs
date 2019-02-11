using BestFood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetFood.Data
{
    public class DbInitializer
    {
        public static void Seed (BestFoodContext ctx)
        {
            ctx.Database.EnsureCreated();
            var hasRestaurant = ctx.Restaurants.Any();
            if (hasRestaurant)
            {
                return;
            }

            ctx.Restaurants.AddRange(
                new Restaurant()
                {
                    Name = "65 Fireflies",
                    Specialties = new List<Specialty>()
                    {
                        new Specialty() {Name = "Glühwein"},
                        new Specialty() {Name = "Pizza"}
                    }
                },
                new Restaurant() { Name = "Rocket" }
                );

            ctx.SaveChanges();
        }
    }
}
