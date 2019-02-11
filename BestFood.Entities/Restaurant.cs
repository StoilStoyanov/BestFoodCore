using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestFood.Entities
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Specialty> Specialties { get; set; } = new HashSet<Specialty>();
    }
}
