using Microsoft.AspNetCore.Mvc;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using ProductQRCodeReadWithDynamic.Repositories.Concretes;

namespace ProductQRCodeReadWithDynamic.Controllers
{
    public class CamController : Controller
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        public CamController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, INotificationWriteRepository notificationWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _notificationWriteRepository = notificationWriteRepository;
        }

        [HttpGet]
        public IActionResult GetCam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCam(string productCode)
        {
            if (productCode != null)
            {
                StaticStrings.Properties.HubProperties.readProductCode = productCode;
                Product product = await _productReadRepository.GetFilter(p => p.ProductCode == productCode && p.IsSuccess == true);
                product.ReadCount += 1;
                bool result = await _productWriteRepository.UpdateAsync(product);
                return RedirectToAction("Index", "Product");
            }
            return View();
        }


        [HttpGet]
        public IActionResult GetCamForUpdated()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCamForUpdated(string updatedCode)
        {
            string newProductCode = _httpContextAccessor.HttpContext.Session.GetString("NewProductCode");
            string newUpdatedCode = _httpContextAccessor.HttpContext.Session.GetString("NewUpdatedCode");
            Product product = await _productReadRepository.GetFilter(p => p.ProductCode == newProductCode && p.UpdatedCode == Guid.Parse(newUpdatedCode) && p.IsSuccess == false);

            if (product.UpdatedCode == Guid.Parse(updatedCode))
            {
                product.IsSuccess = true;
                product.UpdatedDate = DateTime.Now;
                bool result = await _productWriteRepository.UpdateAsync(product);
                if (result)
                {
                    string oldUpdatedCode = _httpContextAccessor.HttpContext.Session.GetString("OldProductCode");
                    Product oldProduct = await _productReadRepository.GetFilter(p => p.ProductCode == oldUpdatedCode && p.IsSuccess == true);
                    oldProduct.IsSuccess = false;
                    bool result2 = await _productWriteRepository.UpdateAsync(oldProduct);
                    await _notificationWriteRepository.AddAsync(new()
                    {
                        Message = "Product Updated Successfully !",
                        MessageType = "Personal",
                        Email = _httpContextAccessor.HttpContext.Session.GetString("Email"),
                        IsSuccess = true
                    });
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                await _notificationWriteRepository.AddAsync(new()
                {
                    Message = "Product Updated Unsuccessfully !!!!",
                    MessageType = "Personal",
                    Email = _httpContextAccessor.HttpContext.Session.GetString("Email"),
                    IsSuccess = true
                });
                return View();
            }
            return View();
        }
    }
}
