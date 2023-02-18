using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Vet.Models;
using Dal;
using Entities.SearchParams;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Vet.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = (await new VetsDal().GetAsync(new VetSearchParams() { Login = model.Login })).Objects.FirstOrDefault();
                if (user != null)
                {
                    if (user.Password == model.Password)
                    {
                        await Authenticate(model.Login, Enum.GetName((RoleType)user.RoleType));

                        if (user.RoleType == RoleType.admin)
                        {
                            VetParams.Id = user.Id;
                            VetParams.Photo = user.PhotoParth;
                            return Redirect("/Admin/Vet/Index");
                        }
                        else
                        {
                            return Redirect("/Worker/Home/Index");
                        }
                    }
                }
            }
            return View(model);
        }
        public async Task Authenticate(string userName, string userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var principal = new ClaimsPrincipal(id);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                IsPersistent = true,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);

        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            VetParams.Id = null;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
