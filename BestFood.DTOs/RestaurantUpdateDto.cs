using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestFood.DTOs
{
    public class RestaurantUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<SpecialtyDto> Specialties { get; set; } = new List<SpecialtyDto>();
    }
}
