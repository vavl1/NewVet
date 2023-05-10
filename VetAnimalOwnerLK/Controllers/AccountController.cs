using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dal;
using Entities.SearchParams;
using VetAnimalOwnerLK.Models;

namespace VetAnimalOwnerLK.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationModel model)
        {

            if (ModelState.IsValid)
            {
                var user = (await new AnimalOwnerDal().GetAsync(new AnimalOwnerSearchParams() { Login = model.Login })).Objects.FirstOrDefault();
                if (user == null)
                {
                  
                        user = new Entities.AnimalOwnerEntity() { Name = model.Name, LastName = model.LastName, FatherName = model.FatherName, Login = model.Login, Password= model.Password };
                    await new AnimalOwnerDal().AddOrUpdateAsync(user);
                        
                        return Redirect("/Account/Login");

                   
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = (await new AnimalOwnerDal().GetAsync(new AnimalOwnerSearchParams() { Login = model.Login })).Objects.FirstOrDefault();
                if (user != null)
                {
                    if (user.Password == model.Password)
                    {
                        await Authenticate(model.Login);

                       
                       
                            AnimalOwnerParams.Id = user.Id;
                           // VetParams.Photo = user.PhotoParth;
                            return Redirect("/Public/Home/Index");
                        
                    }
                }
            }
            return View(model);
        }
        public async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                
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
            AnimalOwnerParams.Id = null;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
