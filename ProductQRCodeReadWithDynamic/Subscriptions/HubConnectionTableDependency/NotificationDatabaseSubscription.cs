using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Hubs;
using TableDependency.SqlClient;

namespace ProductQRCodeReadWithDynamic.Subscriptions.HubConnectionTableDependency
{
    public class NotificationDatabaseSubscription : IDatabaseSubscription
    {
        SqlTableDependency<Notification> _tableDependency;
        NotificationHub _notificationHub;

        public NotificationDatabaseSubscription(NotificationHub notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<Notification>(Configurations.Configuration.ConnectionString, tableName);
            _tableDependency.OnChanged += async (o, e) =>
            {
                var notification = e.Entity;
                if (notification.MessageType == "All")
                {
                    await _notificationHub.SendNotificationToAll(notification.Message);
                }
                else
                {
                    await _notificationHub.SendNotificationToClient(notification.Message, notification.Email);
                }
            };
            _tableDependency.OnError += (o, e) =>
            {

            };
            _tableDependency.Start();
        }
    }
}
