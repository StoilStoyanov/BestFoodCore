using BestFood.Entities;
using BestFood.Services.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using System.Collections.Generic;

namespace BestFood.API.Controllers
{
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[ODataRoutePrefix("Restaurant")]
	public class RestaurantController : ODataController
	{
		private readonly IRestaurantService _restaurantService;

		public RestaurantController(IRestaurantService restaurantService)
		{
			_restaurantService = restaurantService;
		}

		[ODataRoute]
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
