using Microsoft.AspNetCore.Mvc;

namespace ProductQRCodeReadWithDynamic.ViewComponents
{
    public class UpdateForCode : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
