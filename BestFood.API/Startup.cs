using BestFood.Services;
using BestFood.Services.Interfaces;
using BetFood.Data;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;

namespace BestFood.API
{
	public class Startup
	{
		public static IConfiguration Configuration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
					.AddJsonOptions(
					options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
			var connetionString = Configuration["connectionStrings:bestFoodDBConnectionString"];
			services.AddDbContext<BestFoodContext>(x => x.UseSqlServer(connetionString));

			services.AddOData();
			// Fixes swagger.json generation. Workaround: https://github.com/OData/WebApi/issues/1177
			services.AddMvcCore(options =>
			{
				foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
				{
					outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
				}
				foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
				{
					inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
				}
			});

			services.AddTransient<EdmModelBuilder>();
			services.AddScoped<IRestaurantService, RestaurantService>();

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, EdmModelBuilder modelBuilder)
		{
			if (env.IsDevelopment())
			{
				using (var scope = app.ApplicationServices.CreateScope())
				using (var context = scope.ServiceProvider.GetRequiredService<BestFoodContext>())
				{
					DbInitializer.Seed(context);
				}
				app.UseDeveloperExceptionPage();
			}

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseMvc(routeBuilder =>
			{
				routeBuilder.MapODataServiceRoute("ODataRoutes", "odata", modelBuilder.GetEdmModel(app.ApplicationServices));
			});
		}
	}
}
