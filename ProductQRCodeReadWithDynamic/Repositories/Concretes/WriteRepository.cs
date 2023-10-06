using Dapper.Contrib.Extensions;
using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities.Base;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using System.Data;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class WriteRepository<T> : IWriteRepository<T> where T : EntityBase, new()
    {
        protected IDbConnection DbConnection { get; private set; }
        private readonly DbSettings _dbSettings;

        public WriteRepository(DbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            DbConnection = new DbContext().SetStrategy().GetDbContext(_dbSettings.ConnectionString);
        }

        public async Task<bool> AddAsync(T entity)
        {
            DbConnection.Open();
            try
            {
                int instead = await DbConnection.InsertAsync<T>(entity);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { DbConnection.Close(); }
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            DbConnection.Open();

            try
            {
                var entity = await DbConnection.GetAsync<T>(id);

                if (entity == null)
                    return false;
                return await DbConnection.DeleteAsync<T>(entity);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            DbConnection.Open();
            try
            {
                return await DbConnection.UpdateAsync<T>(entity);
            }
            finally { DbConnection.Close(); }
        }
    }
}
