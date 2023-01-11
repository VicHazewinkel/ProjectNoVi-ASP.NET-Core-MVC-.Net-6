using Microsoft.AspNetCore.Mvc;

namespace ProjectNoVi_V3.Web.Controllers
{
    public class ShoppingCartControllerV2 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
