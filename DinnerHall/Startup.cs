using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DinnerHall
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DinnerHall", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/hello", async context =>
                // {
                //     await context.Response.WriteAsync("Hello, World!");
                // });
                endpoints.MapPost("/distribution", async context =>
                {
                    if (!context.Request.HasJsonContentType())
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                        return;
                    }
                    var distributionData = await context.Request.ReadFromJsonAsync<DistributionData>();
                    await context.Response.WriteAsync(distributionData.ToString());
                    //Task.Run(() => );
                        //UpdateDatabaseAsync(weather);
                    

                    context.Response.StatusCode = (int) HttpStatusCode.Accepted;
                });
            });
        }
    }
}
