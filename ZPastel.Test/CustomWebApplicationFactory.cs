using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using ZPastel.API;
using ZPastel.Persistence;
using ZPastel.Test.DbSeed.DataSeed;

namespace ZPastel.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly SqliteConnection connection;

        public CustomWebApplicationFactory()
        {
            connection = new SqliteConnection("DataSource=:memory:");

            connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.Single(d => d.ServiceType == typeof(DbContextOptions<DataContext>)));

                services
                    .AddDbContext<DataContext>(builder =>
                    {
                        builder.UseSqlite(connection);
                        builder.ConfigureWarnings(builder => builder.Ignore(RelationalEventId.AmbientTransactionWarning));
                    });

                services.AddTransient<DataSeedService>();

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var dataSeedService = scope.ServiceProvider.GetService<DataSeedService>();
                dataSeedService.Seed();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            connection.Dispose();
        }
    }
}
