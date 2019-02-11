﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetFood.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc();
            var connetionString = Configuration["connectionStrings:bestFoodDBConnectionString"];
            services.AddDbContext<BestFoodContext>(x => x.UseSqlServer(connetionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseMvc();
        }
    }
}
