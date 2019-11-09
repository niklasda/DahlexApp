using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DahlexApp.Server
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dahlex API", Version = "v1" });
            });
        }

        // Take in Lamar's ServiceRegistry instead of IServiceCollection
        // as your argument, but fear not, it implements IServiceCollection
        // as well
        public void ConfigureContainer(ServiceRegistry services)
        {
            // Supports ASP.Net Core DI abstractions
            //services.AddMvc();
            //services.AddLogging();

            // Also exposes Lamar specific registrations
            // and functionality
            services.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });

            // services.Lama();
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dahlex v1");
            });
        }
    }
}
