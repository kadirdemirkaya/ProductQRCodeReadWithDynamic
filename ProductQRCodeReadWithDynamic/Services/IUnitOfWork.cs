using ProductQRCodeReadWithDynamic.Entities.Base;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Services
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : EntityBase, new();

        IWriteRepository<T> GetWriteRepository<T>() where T : EntityBase, new();
    }
}
