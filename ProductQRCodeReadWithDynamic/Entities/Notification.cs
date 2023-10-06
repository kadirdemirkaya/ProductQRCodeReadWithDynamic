using Dapper.Contrib.Extensions;
using ProductQRCodeReadWithDynamic.Entities.Base;

namespace ProductQRCodeReadWithDynamic.Entities
{
    public class Notification : EntityBase
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public int UpdateCount { get; set; } = 0;
    }
}
