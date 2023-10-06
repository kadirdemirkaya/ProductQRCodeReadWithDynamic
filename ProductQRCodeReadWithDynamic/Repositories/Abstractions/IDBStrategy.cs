using System.Data;

namespace ProductQRCodeReadWithDynamic.Repositories.Abstractions
{
    public interface IDBStrategy
    {
        IDbConnection GetConnection(string connectionString);
    }
}
