using DealHub.Models;
using DealHub.Models.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealHub.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserController(
            UserManager<ApplicationUser> _userManager
            , RoleManager<IdentityRole> _roleManager
            )
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            
            foreach (ApplicationUser user in userManager.Users.Include(u => u.Deals))
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserView _model = new UserView
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(_model);
        }
    }
    public static class Roles
    {
        public const string Admin = "ADMIN";
        public const string Moderateur = "MODERATEUR";
        public const string Marchand = "MARCHAND";
        public const string User = "USER";
    }
}
