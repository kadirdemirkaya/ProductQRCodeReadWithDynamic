using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities.Base;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using ProductQRCodeReadWithDynamic.Repositories.Concretes;
using System.Data;

namespace ProductQRCodeReadWithDynamic.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDbConnection DbConnection { get; private set; }
        private readonly DbSettings _dbSettings;

        public UnitOfWork(DbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            DbConnection = new DbContext().SetStrategy().GetDbContext(_dbSettings.ConnectionString);
        }
        public async ValueTask DisposeAsync()
        {
            DbConnection.Dispose();
        }

        public IReadRepository<T> GetReadRepository<T>() where T : EntityBase, new()
        {
            return new ReadRepository<T>(_dbSettings);
        }

        public IWriteRepository<T> GetWriteRepository<T>() where T : EntityBase, new()
        {
            return new WriteRepository<T>(_dbSettings);
        }
    }
}
