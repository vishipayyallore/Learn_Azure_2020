using CosmosDbDemo1.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CosmosDbDemo1
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

            services.AddDbContext<NoSqlDbContext>(options =>
              options.UseCosmos(
                  //I use user secrets to provide the actual Azure Cosmos database, but fall back to local emulator if no secrets set
                  Configuration["CosmosUrl"] ?? Configuration["endpoint"],
                  Configuration["CosmosKey"] ?? Configuration["authKey"],
                  Configuration["database"]));

            services.AddDbContext<CollegeDbContext>(options =>
                options.UseCosmos(
                    Configuration["CosmosUrl"] ?? Configuration["endpoint"],
                  Configuration["CosmosKey"] ?? Configuration["authKey"],
                  Configuration["database1"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
