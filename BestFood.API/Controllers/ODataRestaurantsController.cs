﻿using BestFood.DTOs;
using BestFood.Entities;
using BestFood.Services.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using System.Collections.Generic;
using System.Linq;

namespace BestFood.API.Controllers
{
	[Produces("application/json")]
	public class RestaurantController : ODataController
	{
		private readonly IRestaurantService _restaurantService;

		public RestaurantController(IRestaurantService restaurantService)
		{
			_restaurantService = restaurantService;
		}

		[Produces("application/json")]
		[ProducesResponseType(typeof(ODataValue<IEnumerable<Restaurant>>), 200)]
		public IActionResult Get(ODataQueryOptions<Restaurant> options)
		{
			var validationSettings = new ODataValidationSettings()
			{
				AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.Count,
				AllowedOrderByProperties = { "Name" },
				AllowedArithmeticOperators = AllowedArithmeticOperators.None,
				AllowedFunctions = AllowedFunctions.None,
				AllowedLogicalOperators = AllowedLogicalOperators.None,
				MaxOrderByNodeCount = 2,
				MaxTop = 100,
			};

			try
			{
				options.Validate(validationSettings);
			}
			catch (ODataException)
			{
				return BadRequest();
			}

			return Ok(options.ApplyTo(_restaurantService.GetAll()));
		}

	}
}
