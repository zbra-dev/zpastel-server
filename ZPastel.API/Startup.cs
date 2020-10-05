using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using ZPastel.API.Converters;
using ZPastel.Persistence;
using ZPastel.Persistence.Contract;
using ZPastel.Persistence.Impl;
using ZPastel.Service.Contract;
using ZPastel.Service.Impl;

namespace ZPastel.API
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

            services.AddTransient<IPastelService, PastelService>();
            services.AddTransient<IPastelRepository, PastelRepository>();
            services.AddTransient<PastelConverter, PastelConverter>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));
            }, ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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

            InitDatabase(serviceProvider);
        }

        private void InitDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                logger.LogInformation("Initializing Database...");

                logger.LogInformation("Executing Database Migration...");
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                if (!context.Database.IsInMemory() && !context.Database.IsSqlite())
                {
                    context.Database.Migrate();
                }

                context.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
                logger.LogInformation("Done initializing Database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                throw;
            }
        }
    }
}
