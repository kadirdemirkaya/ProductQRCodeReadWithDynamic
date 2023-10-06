using Microsoft.AspNetCore.SignalR;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Hubs;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using ProductQRCodeReadWithDynamic.Services.Abstractions.Hubs;

namespace ProductQRCodeReadWithDynamic.Services.Concretes.Hubs
{
    public class ProductHubService : IProductHubService
    {
        private readonly IHubContext<ProductHub> _hubContext;
        private readonly IProductReadRepository _productReadRepository;

        public ProductHubService(IHubContext<ProductHub> hubContext, IProductReadRepository productReadRepository)
        {
            _hubContext = hubContext;
            _productReadRepository = productReadRepository;
        }
        public async Task RedirectWithProductCode()
        {
            //string? sessionProductCode = _httpContextAccessor.HttpContext.Session.GetString("ReadProductCode");
            string? sessionProductCode = StaticStrings.Properties.HubProperties.readProductCode;
            var product = await _productReadRepository.GetFilter(p => p.ProductCode == sessionProductCode && p.IsSuccess == true);
            if (product is not null)
            {
                int productId = product.Id;
                await _hubContext.Clients.All.SendAsync(StaticStrings.Properties.HubProperties.receiveProductCode, productId);
            }
        }

        public async Task AllProductAsync()
        {
            List<Product> products = await _productReadRepository.FindAllAsync();
            await _hubContext.Clients.All.SendAsync(StaticStrings.Properties.HubProperties.receiveAllProduct, products);
        }
    }
}
