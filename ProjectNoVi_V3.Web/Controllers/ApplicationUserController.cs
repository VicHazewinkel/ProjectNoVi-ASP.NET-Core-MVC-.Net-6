using Microsoft.AspNetCore.Mvc;

namespace ProjectNoVi_V3.Web.Controllers
{
    public class ApplicationUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
