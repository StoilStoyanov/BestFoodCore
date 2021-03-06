﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace BestFood.DTOs
{
    public class RestaurantDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SpecialtyDto> Specialties { get; set; } = new List<SpecialtyDto>();
    }
}
