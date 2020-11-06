using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using ZPastel.API.Converters;
using ZPastel.Core.Repositories;
using ZPastel.API.Filters;
using ZPastel.Persistence;
using ZPastel.Persistence.Impl;
using ZPastel.Service.Contract;
using ZPastel.Service.Impl;
using ZPastel.Service.Validators;

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
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                });

            services.AddTransient<IPastelService, PastelService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPastelRepository, PastelRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<PastelConverter>();
            services.AddTransient<OrderConverter>();
            services.AddTransient<UpdateOrderConverter>();
            services.AddTransient<UpdateOrderItemConverter>();
            services.AddTransient<OrderItemConverter>();
            services.AddTransient<OrderItemValidator>();
            services.AddTransient<UpdateOrderValidator>();
            services.AddTransient<OrderValidator>();

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
