namespace ProductQRCodeReadWithDynamic.Subscriptions
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
}
