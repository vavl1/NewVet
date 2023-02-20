using Dal;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {

       
      
            public async Task<IActionResult> EditVet(int id)
            {
                var vet = await new VetsDal().GetAsync(id);
                return View(vet);
            }
            [HttpPost]
            public async Task<IActionResult> EditVet(VetEntity model)
            {
                if (ModelState.IsValid)
                {
                var role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
                
                await new VetsDal().AddOrUpdateAsync(model);
                if (role == "admin")
                {
                    return Redirect("/Admin/Vet/Index");
                }
                else
                {
                    return Redirect("/Admin/Worker/Index/"+model.Id);
                }   
                }
                else
                {
                    return View(model);
                }

            }
    }
    
}
