using Dal;
using Dal.DbModels;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;
using Vet.Areas.Admin.Models;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AnimalOwnerController : Controller
    {
        IWebHostEnvironment test;
        public AnimalOwnerController(IWebHostEnvironment test)
        {
            this.test = test;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            var pageSize = 6;
            var animalOwners = (await new AnimalOwnerDal().GetAsync(new AnimalOwnerSearchParams())).Objects;
            var count = animalOwners.Count;
            var items = animalOwners.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(new PageModel<AnimalOwnerModel>(count, page, pageSize, items.Select(i => new AnimalOwnerModel
            {
                Name = i.Name,
                Adress = i.Adress,
                FatherName = i.FatherName,
                Id = i.Id,
                LastName = i.LastName,
                Phone = i.Phone,
                CountAnimals = i.Animals.Count
            }).ToList())); 
            


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
        [HttpPost]
        public async Task<string> AddPhoto(IFormFile File)
        {
            if (File != null)
            {
                var parth = "/images/" + File.FileName;
                using (var fileStream = new FileStream(test.WebRootPath + parth, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
                return parth;
            }
            else
            {
                return "";
            }



        }
    }
}
