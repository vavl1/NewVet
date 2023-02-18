using Dal;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class VetController : Controller
    {
        IWebHostEnvironment test;
        public VetController(IWebHostEnvironment test)
        {
            this.test = test;
        }
        
        public async Task<IActionResult> Index()
        {
            var vets = (await new VetsDal().GetAsync(new VetSearchParams())).Objects.ToList();
           
            return View(vets);
        }
        public async Task<IActionResult> DeleteVet(int id)
        {
            await new VetsDal().DeleteAsync(id);

            return Redirect("/Admin/Vet/Index");
        }
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
                await new VetsDal().AddOrUpdateAsync(model);
                return Redirect("/Admin/Vet/Index");
            }
            else
            {
                return View(model);
            }
           
        }
        public  IActionResult AddVet()
        {
            
            return View(new VetEntity());
        }
        [HttpPost]
        public async Task<IActionResult> AddVet(VetEntity model)
        {
            if (ModelState.IsValid)
            {
                await new VetsDal().AddOrUpdateAsync(model);
                return Redirect("/Admin/Vet/Index");
            }
            else
            {
                return View(model);
            }

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
                return  parth;
            }
            else
            {
                return "";
            }
           
                
        
        

            
            
            
        }
    }
}
