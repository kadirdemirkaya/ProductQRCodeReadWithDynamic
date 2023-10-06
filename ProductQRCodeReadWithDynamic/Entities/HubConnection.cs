using ProductQRCodeReadWithDynamic.Entities.Base;

namespace ProductQRCodeReadWithDynamic.Entities
{
    public class HubConnection : EntityBase
    {
        public string ConnectionId { get; set; }
        public string Email { get; set; } = "NONE";
    }
}
