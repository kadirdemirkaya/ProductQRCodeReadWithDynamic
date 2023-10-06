namespace ProductQRCodeReadWithDynamic.Services.Abstractions
{
    public interface IQrCodeService
    {
        Task<bool> QrCodeSave(int id);

        public (byte[] byteArray, string fileType) EncryptedQrCode(string inputText);

        byte[] EncryptedQrCodeForUpdated(string inputText);

    }
}
