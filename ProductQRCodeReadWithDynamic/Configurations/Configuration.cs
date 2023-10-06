namespace ProductQRCodeReadWithDynamic.Configurations
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                return configuration.GetConnectionString("SqlConnectionString");
            }
        }
    }
}
