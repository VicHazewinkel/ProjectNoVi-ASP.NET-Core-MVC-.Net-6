using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ProjectNoVi_V3.Web.Controllers
{
    public class SalesController : Controller
    {
        [Authorize(Roles = "Optometrist,Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
