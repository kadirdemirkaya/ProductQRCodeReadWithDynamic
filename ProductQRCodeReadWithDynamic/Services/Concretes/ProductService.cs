using ProductQRCodeReadWithDynamic.Services.Abstractions;
using QRCoder;
using System.Drawing;

namespace ProductQRCodeReadWithDynamic.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
