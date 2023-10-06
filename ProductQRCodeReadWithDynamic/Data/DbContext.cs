using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using System.Data;

namespace ProductQRCodeReadWithDynamic.Data
{
    public class DbContext
    {
        private IDBStrategy _dbStrategy;

        public DbContext SetStrategy()
        {
            _dbStrategy = new SqlServerStrategy();
            return this;
        }

        public IDbConnection GetDbContext(string connectionString)
        {
            return _dbStrategy.GetConnection(connectionString);
        }
    }
}
