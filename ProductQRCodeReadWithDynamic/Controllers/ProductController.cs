using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductQRCodeReadWithDynamic.Data;
using ProductQRCodeReadWithDynamic.Entities;
using ProductQRCodeReadWithDynamic.Models.Product;
using ProductQRCodeReadWithDynamic.Repositories.Abstractions;
using ProductQRCodeReadWithDynamic.Services.Abstractions;
using ProductQRCodeReadWithDynamic.Services.Abstractions.Hubs;
using System.Drawing;

namespace ProductQRCodeReadWithDynamic.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IProductHubService _productHubService;
        private readonly AppDbContext _context;
        private readonly IQrCodeService _qrCodeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;
        public ProductController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, IProductService productService, IProductHubService productHubService, AppDbContext context, IQrCodeService qrCodeService, IHttpContextAccessor httpContextAccessor, IMailService mailService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
            _productService = productService;
            _productHubService = productHubService;
            _context = context;
            _qrCodeService = qrCodeService;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productReadRepository.GetFilterAll(p => p.IsSuccess == true);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel addProductViewModel)//nesne alacak
        {
            Product product = _mapper.Map<Product>(addProductViewModel);
            bool result = await _productWriteRepository.AddAsync(product);
            if (result)
            {
                await _context.Set<Notification>().AddAsync(new() { Message = "SIGNOUT Succesfully", IsSuccess = true, Email = HttpContext.Session.GetString("Email"), MessageType = "Personal", CreatedDate = DateTime.Now });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailProduct(int id)
        {
            Product product = await _productReadRepository.FindByIdAsync(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult CodeForProduct(string productCode)
        {
            var tuple = _productService.EncryptedQrCode(productCode);
            return File(tuple.byteArray, tuple.fileType);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool result = await _productWriteRepository.DeleteAsync(id);
            if (result)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            Product product = await _productReadRepository.FindByIdAsync(id);
            HttpContext.Session.SetString("productId", id.ToString());
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(UpdateProductViewModel updateProductViewModel)
        {
            _httpContextAccessor.HttpContext.Session.SetString("OldProductCode", updateProductViewModel.ProductCode); //eski
            var map = _mapper.Map<Product>(updateProductViewModel);
            map.IsSuccess = false;
            map.UpdatedCode = Guid.NewGuid();
            map.ProductCode = Guid.NewGuid().ToString();

            _httpContextAccessor.HttpContext.Session.SetString("NewUpdatedCode", map.UpdatedCode.ToString()); //yeni
            _httpContextAccessor.HttpContext.Session.SetString("NewProductCode", map.ProductCode);      //yeni

            //-----------------

            string imagePath = StaticStrings.Properties.Paths.filePath;

            byte[] bytes = _qrCodeService.EncryptedQrCodeForUpdated(map.UpdatedCode.ToString());

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
                image = Image.FromStream(ms);

            string imagePathName = Path.Combine(imagePath, $"{Guid.NewGuid().ToString()}.png");

            image.Save(imagePathName, System.Drawing.Imaging.ImageFormat.Png);

            map.ImageCodePath = imagePathName;

            bool result = await _productWriteRepository.AddAsync(map);

            //------------------

            await _mailService.SendMessageWithImageAsync(new[] { _httpContextAccessor.HttpContext.Session.GetString("Email") }, "Update Correct Maili", "<strong>Product Updated Correct</strong>", true, map.ImageCodePath);

            if (result)
                return RedirectToAction("GetCamForUpdated", "Cam");
            return View();
        }
    }
}
