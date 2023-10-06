using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(DbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
