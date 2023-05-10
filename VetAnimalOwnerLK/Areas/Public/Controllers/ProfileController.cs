using Dal;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VetAnimalOwnerLK.Areas.Public.Controllers
{
    [Area("Public")]
    public class ProfileController : Controller
    {
        public async Task<IActionResult> EditAnimalOwner()
        {
            var animalOwner = await new AnimalOwnerDal().GetAsync(AnimalOwnerParams.Id.GetValueOrDefault());
            return View(animalOwner);
        }
        [HttpPost]
        public async Task<IActionResult> EditAnimalOwner(AnimalOwnerEntity model)
        {
            if (ModelState.IsValid)
            {
                var role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;

                await new AnimalOwnerDal().AddOrUpdateAsync(model);
               
                    return Redirect("/Public/Home/Index");
                
                
            }
            else
            {
                return View(model);
            }

        }
    }
}
