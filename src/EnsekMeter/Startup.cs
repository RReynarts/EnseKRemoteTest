using System.Diagnostics.CodeAnalysis;
using Common.IoC;
using EnsekMeter.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.IoC;
using Services.IoC;

namespace EnsekMeter
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEnsekCommon();
            services.AddEnsekServices();
            services.AddEnsekRepository();
            services.AddDbContext<EnsekDbContext>(options => options.UseInMemoryDatabase("EnsekMeter"));
            //var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            //services.AddDbContext<EnsekDbContext>(options => options.UseSqlServer(connectionString));
            services.AddMvc().AddMvcOptions(o => o.Filters.Add(new ApiExceptionFilterAttribute(false)));
            ConfigureSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UseSwagger(app);

            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            if (serviceScope == null) return;
            var context = serviceScope.ServiceProvider.GetRequiredService<EnsekDbContext>();
            if (!context.Database.IsInMemory())
            {
                context.Database.Migrate();
            }
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ensek Meter API", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
            });
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ensek Meter V1");
                c.EnableDeepLinking();

            });
        }
    }
}
