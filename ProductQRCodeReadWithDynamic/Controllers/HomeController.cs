using Microsoft.AspNetCore.Mvc;
using ProductQRCodeReadWithDynamic.Models;
using QRCoder;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProductQRCodeReadWithDynamic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ReadCode()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateQRCode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateQRCode(string inputText)
        {
            using (MemoryStream ms = new())
            {
                QRCodeGenerator qRCodeGenerator = new();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                QRCode oQrCode = new(qRCodeData);
                Bitmap bitmap = oQrCode.GetGraphic(15);
                var bitmapBytes = ConvertBitmapToBytes(bitmap);
                return File(bitmapBytes, "image/jpeg");
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