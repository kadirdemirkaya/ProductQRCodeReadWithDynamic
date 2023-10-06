using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Repositories.Concretes
{
    public class NotificationReadRepository : ReadRepository<Notification>, INotificationReadRepository
    {
        public NotificationReadRepository(DbSettings dbSettings) : base(dbSettings)
        {
        }
    }
}
