using BestFood.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetFood.Data
{
    public class BestFoodContext: DbContext
    {
        public BestFoodContext(DbContextOptions<BestFoodContext> options)
            : base(options)
        {
            Database.Migrate ();
        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Specialty> Specialties { get; set; }
    }
}
