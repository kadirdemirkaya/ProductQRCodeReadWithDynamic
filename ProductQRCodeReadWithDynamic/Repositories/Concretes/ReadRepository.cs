using Dapper;
using Dapper.Contrib.Extensions;
using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities.Base;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using System.Data;
using System.Linq.Expressions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class ReadRepository<T> : IReadRepository<T> where T : EntityBase, new()
    {
        protected IDbConnection DbConnection { get; private set; }
        private readonly DbSettings _dbSettings;

        public ReadRepository(DbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            DbConnection = new DbContext().SetStrategy().GetDbContext(_dbSettings.ConnectionString);
        }

        public async Task<List<T>> FindAllAsync()
        {
            DbConnection.Open();
            try
            {
                var results = await DbConnection.GetAllAsync<T>();

                return results.ToList();
            }
            finally { DbConnection.Close(); }
        }

        public async Task<T> FindByIdAsync(int id)
        {
            DbConnection.Open();
            try
            {
                return await DbConnection
                    .GetAsync<T>(id);
            }
            finally { DbConnection.Close(); }
        }

        public async Task<List<T>> GetFilterAll(Expression<Func<T, bool>> filter)
        {
            DbConnection.Open();
            try
            {
                var data = await DbConnection.GetAllAsync<T>();
                var results = data.AsQueryable().Where(filter).ToList();
                return results;
            }
            finally
            {
                DbConnection.Close();
            }
        }

        public async Task<T> GetFilter(Expression<Func<T, bool>> filter)
        {
            DbConnection.Open();
            try
            {
                var data = await DbConnection.GetAllAsync<T>();
                var results = data.AsQueryable().SingleOrDefault(filter);
                return results;
            }
            finally
            {
                DbConnection.Close();
            }
        }

        public async Task<List<T>> GetQueryAll(string query)
        {
            DbConnection.Open();
            try
            {
                var data = await DbConnection.QueryAsync<T>(query);
                return data.ToList();
            }
            finally
            {
                DbConnection.Close();
            }
        }

        public async Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters)
        {
            DbConnection.Open();
            try
            {
                var results = await DbConnection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
                return results;
            }
            finally
            {
                DbConnection.Close();
            }
        }
    }
}
