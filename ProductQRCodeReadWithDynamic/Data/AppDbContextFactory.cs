using Microsoft.EntityFrameworkCore;

namespace ProductQRCodeReadWithDynamic.Data
{
    public class AppDbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlConnectionString"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
