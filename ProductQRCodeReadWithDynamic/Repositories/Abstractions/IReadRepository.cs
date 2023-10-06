using Dapper;
using ProductQRCodeReadWithDynamic.Entities.Base;
using System.Linq.Expressions;

namespace ProductQRCodeReadWithDynamic.Repositories.Abstractions
{
    public interface IReadRepository<T> where T : EntityBase, new()
    {
        Task<List<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);


        Task<List<T>> GetFilterAll(Expression<Func<T, bool>> filter);
        Task<T> GetFilter(Expression<Func<T, bool>> filter);
        Task<List<T>> GetQueryAll(string query);
        Task<int> GetStoredProcedure(string storedProcedure, DynamicParameters dynamicParameters);
    }
}
