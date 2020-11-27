using Microsoft.EntityFrameworkCore;
using ZPastel.Persistence;
using ZPastel.Test.DbSeed.DataSeed;

namespace ZPastel.Test
{
    internal class DataContextFactory
    {
        public DataContext CreateSeededDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            var dataContext = new DataContext(options);
            new DataSeedService(dataContext).Seed();

            return dataContext;
        }
    }
}
