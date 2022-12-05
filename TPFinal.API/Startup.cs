

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TPFinal.API.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TPFinal.API
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddMvcOptions(options => options.EnableEndpointRouting = false);
            // Add configuration for DbContext
            // Use connection string from appsettings.json file
            /*
              services.AddDbContext<Context>(options =>
              {
                  options.UseSqlServer(Configuration["AppSettings:ConnectionString"]);
              });
           */
            var connection = @"Server=(localdb)\mssqllocaldb;Database=TpFinal;Trusted_Connection=True;";
            //Server =(localdb)\mssqllocaldb;Database=AspCore_NovoDB;Trusted_Connection=True;";

            services.AddDbContext<Context>(options => options.UseSqlServer(connection));
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = " API",
                    Version = "v1",
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", " API V1");
            });
            app.UseMvc();
        }
    }
#pragma warning restore CS1591
}