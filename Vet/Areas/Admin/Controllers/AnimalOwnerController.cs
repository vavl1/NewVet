using Dal;
using Dal.DbModels;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vet.Areas.Admin.Models;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AnimalOwnerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var animalOwners = (await new AnimalOwnerDal().GetAsync(new AnimalOwnerSearchParams())).Objects;
            return View(animalOwners.Select(i => new AnimalOwnerModel
            {
                Name = i.Name,
                Adress = i.Adress,
                FatherName = i.FatherName,
                Id = i.Id,
                LastName = i.LastName,
                Phone = i.Phone,
                CountAnimals = i.Animals.Count
            }).ToList()) ;
        }
        public IActionResult AddAnimalOwner()
        {
            return View(new AnimalOwnerEntity());
        }
        [HttpPost]
        public async Task<IActionResult> AddAnimalOwner(AnimalOwnerEntity model)
        {
            if (ModelState.IsValid)
            {
                await new AnimalOwnerDal().AddOrUpdateAsync(model);
                return Redirect("/Admin/AnimalOwner/Index");
            }
            else
            {
                return View(model);
            }

        }
        public async Task<IActionResult> DeleteAnimalOwner(int id)
        {
            await new AnimalOwnerDal().DeleteAsync(id);

            return Redirect("/Admin/AnimalOwner/Index");
        }
        public async Task<IActionResult> DetailsAnimalOwner(int id)
        {
            ViewBag.Mail = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "Мужской",
                    Value ="true"
                },
                new SelectListItem()
                {
                    Text = "Женский",
                    Value ="false"
                }
            };
            ViewBag.AnimalOwnerId = id;
            var pets = (await new AnimalDal().GetAsync(new AnimalSearchParams() { AnimalOwnerId = id })).Objects.ToList();
            
            pets = pets.Select(i => new AnimalEntity
            {
                Id = i.Id,
                AnimalOwner = i.AnimalOwner,
                VetId = i.VetId,
                AnimalOwnerNavigation = i.AnimalOwnerNavigation,
                Birthay = i.Birthay,
                Gender = i.Gender,
                Breed = i.Breed,
                NickName = i.NickName,
                Treatments = i.Treatments,
                Vet = i.Vet,
                Diagnoses = i.Diagnoses?.OrderByDescending(j=> j.Date).ToList()
            }

           ).ToList();
            return View(pets);
        }
        [HttpPost]
        public async Task SaveAnimal(AnimalEntity animals)
        {
            if (ModelState.IsValid)
            {
                var id = await new AnimalDal().AddOrUpdateAsync(animals);
                var animal = await new AnimalDal().GetAsync(id);

            }

        }


        
        public async Task<ActionResult> DeleteAnimal(int? id, int ownerId)
        {
            if (id != null)
            {

                var animal = await new AnimalDal().DeleteAsync(id.GetValueOrDefault());
                

            }
          return  Redirect("/Admin/AnimalOwner/DetailsAnimalOwner/" + ownerId);


        }
    }
}
