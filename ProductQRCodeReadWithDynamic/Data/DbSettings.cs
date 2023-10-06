namespace ProductQRCodeReadWithDynamic.Data
{
    public class DbSettings
    {
        IConfiguration _configuration;
        public string ConnectionString { get; set; }
        public DbSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("SqlConnectionString");
        }
    }
}
