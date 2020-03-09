using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiskFirst.Hateoas.BasicSample.netcore3.Models;
using RiskFirst.Hateoas.BasicSample.netcore3.Repository;
using RiskFirst.Hateoas.Models;

namespace RiskFirst.Hateoas.BasicSample.netcore3
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
            services.AddLinks(config =>
            {
                // Uncomment the next line to use relative hrefs instead of absolute
                //config.UseRelativeHrefs();

                config.AddPolicy<ValueInfo>(policy =>
                {
                    policy.RequireRoutedLink("self", "GetValueByIdRoute", x => new { id = x.Id });
                });
                config.AddPolicy<ValueInfo>("FullInfoPolicy", policy =>
                {
                    policy.RequireSelfLink()
                        .RequireRoutedLink("update", "GetValueByIdRoute", x => new { id = x.Id })
                        .RequireRoutedLink("delete", "DeleteValueRoute", x => new { id = x.Id })
                        .RequireRoutedLink("all", "GetAllValuesRoute");
                });

                config.AddPolicy<ItemsLinkContainer<ValueInfo>>(policy =>
                {
                    policy.RequireSelfLink()
                        .RequireRoutedLink("insert", "InsertValueRoute");
                });
            });

            services.AddControllers();

            services.AddApiVersioning();

            services.AddSingleton<IValuesRepository, ValuesRepository>();
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