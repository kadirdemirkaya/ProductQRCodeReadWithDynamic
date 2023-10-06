using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Services.Abstractions.Hubs;
using TableDependency.SqlClient;

namespace ProductQRCodeReadWithDynamic.Subscriptions.ProductTableDependency
{
    public class DatabaseSubscription : IDatabaseSubscription
    {
        #region YENI
        SqlTableDependency<Product> _tableDependency;
        private readonly IServiceProvider _serviceProvider;
        public DatabaseSubscription(AppDbContext context, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<Product>(Configurations.Configuration.ConnectionString, tableName);
            _tableDependency.OnChanged += async (o, e) =>
            {
                if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var productHubService = scope.ServiceProvider.GetService<IProductHubService>();
                        await productHubService.RedirectWithProductCode();

                        await productHubService.AllProductAsync();
                    }
                }
            };
            _tableDependency.OnError += (o, e) =>
            {

            };
            _tableDependency.Start();
        }
        #endregion
    }
}
