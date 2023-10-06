using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;

namespace ProductQRCodeReadWithDynamic.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IHubConnectionWriteRepository _hubConnectionWriteRepository;
        private readonly IHubConnectionReadRepository _hubConnectionReadRepository;
        private readonly AppDbContext _context;
        public NotificationHub(IHubConnectionWriteRepository hubConnectionWriteRepository, IHubConnectionReadRepository hubConnectionReadRepository, AppDbContext context)
        {
            _hubConnectionWriteRepository = hubConnectionWriteRepository;
            _hubConnectionReadRepository = hubConnectionReadRepository;
            _context = context;
        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string email)
        {
            var hubConnections = await _hubConnectionReadRepository.GetFilterAll(con => con.Email == email);
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, email);
            }
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string? email)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new()
            {
                ConnectionId = connectionId,
                Email = email
            };

            await _context.Set<HubConnection>().AddAsync(hubConnection);
            //await _hubConnectionWriteRepository.AddAsync(hubConnection);
            await _context.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            HubConnection hConnection = _context.Set<HubConnection>().Where(n => n.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (hConnection is not null)
                _context.Set<HubConnection>().Remove(hConnection);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
