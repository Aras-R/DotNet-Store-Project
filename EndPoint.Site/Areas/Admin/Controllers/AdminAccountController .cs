using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Users.Commands.UserLogin;
using System.Security.Claims;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly IUserLoginService _userLoginService;

        public AdminAccountController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = "/Admin")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = _userLoginService.Execute(email, password);

            if (!result.IsSuccess || !result.Data.Roles.Contains("Admin"))
            {
                return Json(new { isSuccess = false, message = "ایمیل یا رمز عبور اشتباه است یا دسترسی ندارید." });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, result.Data.Name),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(2)
                });

            return Json(new { isSuccess = true, message = "ورود با موفقیت انجام شد." });
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "AdminAccount", new { area = "Admin" });
        }
    }
}
