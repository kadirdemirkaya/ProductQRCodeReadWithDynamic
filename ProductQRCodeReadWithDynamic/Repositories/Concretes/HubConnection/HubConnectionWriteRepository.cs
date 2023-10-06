using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class HubConnectionWriteRepository : WriteRepository<HubConnection>, IHubConnectionWriteRepository
    {
        public HubConnectionWriteRepository(DbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
