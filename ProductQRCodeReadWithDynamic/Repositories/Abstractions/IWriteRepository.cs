using ProductQRCodeReadWithDynamic.Entities.Base;

namespace ProductQRCodeReadWithDynamic.Repositories.Abstractions
{
    public interface IWriteRepository<T> where T : EntityBase, new()
    {
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
