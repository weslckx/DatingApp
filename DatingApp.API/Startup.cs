using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Dependency injection container -> injects services to other parts of our app
        // only service right now: adding controllers
        public void ConfigureServices(IServiceCollection services)
        {
            // we first made a datacontext; now we need to add the 'service' to the app
            //services.AddDbContext<DataContext>(x =>x.UseSqlite("ConnectionString");
            services.AddDbContext<DataContext>(x =>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            // adding db in appsettigns
            services.AddControllers();

            // after angular; we need cors, so adding service
            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // adding 'middleware' for http-requests
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if in development env, we get a exeptionpage when exception
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection(); // any http automaticaly to https
            // for now; not: otherwise we have to work with self signed certificates

// how get our request to a specific method
            app.UseRouting();

            // add after userouting and before useendpoints
            // for now, allow any origins; others client app won't be allowed
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

// app.UseMvc(); will rout request to controller,

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
