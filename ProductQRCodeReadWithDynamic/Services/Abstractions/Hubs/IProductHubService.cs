namespace ProductQRCodeReadWithDynamic.Services.Abstractions.Hubs
{
    public interface IProductHubService
    {
        Task RedirectWithProductCode();

        Task AllProductAsync();
    }
}
