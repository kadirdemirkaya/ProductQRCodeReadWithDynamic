using Microsoft.Data.SqlClient;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using System.Data;

namespace ProductQRCodeReadWithDynamic.Data
{
    public class SqlServerStrategy : IDBStrategy
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
