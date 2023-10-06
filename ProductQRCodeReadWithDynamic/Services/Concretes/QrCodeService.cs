using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using ProductQRCodeReadWithDynamic.Services.Abstractions;
using QRCoder;
using System.Drawing;

namespace ProductQRCodeReadWithDynamic.Services.Concretes
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMailService _mailService;

        public QrCodeService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IMailService mailService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _mailService = mailService;
        }

        public async Task<bool> QrCodeSave(int id)
        {
            Guid updatedCode = Guid.NewGuid();
            byte[] qrCodeBytes = EncryptedQrCodeForUpdated(updatedCode.ToString());

            bool result = false;

            Image qrCodeImage;
            using (MemoryStream memoryStream = new(qrCodeBytes))
            {
                qrCodeImage = Image.FromStream(memoryStream);
                string filePath = $"{StaticStrings.Properties.Paths.filePath}/{Guid.NewGuid()}";
                qrCodeImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                Product product = await _productReadRepository.GetFilter(p => p.Id == id);
                product.ImageCodePath = filePath;
                product.UpdatedCode = updatedCode;
                result = await _productWriteRepository.UpdateAsync(product);
            }
            return result;
        }


        public byte[] EncryptedQrCodeForUpdated(string inputText)
        {
            using (MemoryStream ms = new())
            {
                QRCodeGenerator qRCodeGenerator = new();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                QRCode oQrCode = new(qRCodeData);
                Bitmap bitmap = oQrCode.GetGraphic(15);
                var bitmapBytes = ConvertBitmapToBytes(bitmap);
                return bitmapBytes;
            }
        }

        public (byte[] byteArray, string fileType) EncryptedQrCode(string inputText)
        {
            using (MemoryStream ms = new())
            {
                QRCodeGenerator qRCodeGenerator = new();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                QRCode oQrCode = new(qRCodeData);
                Bitmap bitmap = oQrCode.GetGraphic(15);
                var bitmapBytes = ConvertBitmapToBytes(bitmap);
                string fileType = "image/jpeg";
                return (bitmapBytes, fileType);
            }
        }

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream ms = new())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
