using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class HubConnectionReadRepository : ReadRepository<HubConnection>, IHubConnectionReadRepository
    {
        public HubConnectionReadRepository(DbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
