using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectNoVi_V3.Models;
using ProjectNoVi_V3.Web.ViewModels;
using System.Collections.Generic;

namespace ProjectNoVi_V3.Web.Controllers
{
    public class ApplicationUserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            
            List<ApplicationUserViewModel> list = new();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                list.Add(new ApplicationUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = role.FirstOrDefault(),
                }); 
            }

            return View(list);
        }
    }
}
