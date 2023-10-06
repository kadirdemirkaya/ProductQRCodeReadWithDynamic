using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(DbSettings dbSettings) : base(dbSettings)
        {
        }

    }
}
