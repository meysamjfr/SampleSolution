using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IAuthenticationService _authService;
        private readonly SignInManager<ExtendedUser> _signInManager;
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<ExtendedUser> signInManager,
            UserManager<ExtendedUser> userManager,
            //IAuthenticationService authService,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            //_authService = authService;
        }

        public async Task<IActionResult> Index(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl) == false && returnUrl.Contains("provinces"))
            {
                //var isLoggedIn = await _authService.Authenticate("admin", "Z!l10w");
                //var isLoggedIn = await _authService.Authenticate("meysamjfr", "1qaz2wsx!QAZ@WSX");
                var findUser = await _userManager.FindByNameAsync("admin");
                if (User.Identity.IsAuthenticated)
                {
                    return Content("asdas");
                }
                var isLoggedIn = await _signInManager.PasswordSignInAsync(findUser, "Z!l10w", true, false);
                if (isLoggedIn.Succeeded)
                {
                    //await _userManager.UpdateAsync(findUser);
                    return Json(findUser);
                }
                else
                {
                    return Json(isLoggedIn);
                }
            }

            return Json(returnUrl);
        }
    }
}