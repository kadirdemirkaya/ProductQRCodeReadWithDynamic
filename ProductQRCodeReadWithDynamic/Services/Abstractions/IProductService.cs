namespace ProductQRCodeReadWithDynamic.Services.Abstractions
{
    public interface IProductService
    {
        public (byte[] byteArray, string fileType) EncryptedQrCode(string inputText);
    }
}
