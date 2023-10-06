using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class NotificationWriteRepository : WriteRepository<Notification>, INotificationWriteRepository
    {
        public NotificationWriteRepository(DbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
