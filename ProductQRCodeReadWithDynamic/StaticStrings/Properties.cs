using Microsoft.Extensions.Hosting.Internal;

namespace ProductQRCodeReadWithDynamic.StaticStrings
{
    public static class Properties
    {
        public static class HubProperties
        {
            public static string receiveProductCode = "receiveProductCode";
            public static string receiveAllProduct = "receiveAllProduct";
            public static string receiveProductPath = "receiveProductPath";
            public static string readProductCode = "ReadProductCode";
            public static string sendProductMessage = "sendProductMessage";
        }

        public static class Paths
        {
            public static string filePath = @$"C:/Users/Casper/Desktop/GitHub Projects/ProductQRCodeReadWithDynamic/ProductQRCodeReadWithDynamic/wwwroot/Images";
        }
    }
}
