using Dal;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Linq;

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
        
        public async Task<IActionResult> Index(DateTime? date , bool isFree)
        {
            var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            date = date ?? dateNow;
                var vets = (await new VetsDal().GetAsync(new VetSearchParams() {Role = RoleType.vet })).Objects.ToList();
            if (isFree)
            {
                vets.Where(i => i.Inspections.Where(j => j.Date == date).Count() <= 3);
            }
           
            var animals = (await new AnimalOwnerDal().GetAsync(new AnimalOwnerSearchParams())).Objects.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.LastName + " " + i.Name + " " + i.FatherName }).ToList();
            animals.Add(new SelectListItem { Selected = true, Text = "выберите запись", Value = null });
            ViewBag.AnimalOwners = animals;
            ViewBag.Date = date.GetValueOrDefault().ToShortDateString();
            ViewBag.IsFree = isFree;
            return View(vets);
        }
        public async Task<IActionResult> DeleteVet(int id)
        {
            try
            {
                await new VetsDal().DeleteAsync(id);
            }
            catch(Exception ex)
            {

            }

            return Redirect("/Admin/Vet/Index");
        }
        public async Task<IActionResult> DeleteInspection(int id)
        {
            try
            {
                await new InspectionDal().DeleteAsync(id);
            }
            catch(Exception ex)
            {

            }
            

            return Redirect("/Admin/Vet/Index");
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
        public async Task<IActionResult> CreateInspection(InspectionEntity model, TimeSpan Time)
        {
            if(ModelState.IsValid)
            {

                model.Date = new DateTime(model.Date.Value.Year, model.Date.Value.Month, model.Date.Value.Day, Time.Hours, Time.Minutes, Time.Seconds);
                await new InspectionDal().AddOrUpdateAsync(model);
            }
            return Redirect("/Admin/Vet/Index");

        }
        [HttpPost]
        public async Task<string> GetAnimalAjax(int? id)
        {
            if (id != null) {
                var animals = (await new AnimalDal().GetAsync(new AnimalSearchParams() { AnimalOwnerId = id })).Objects.ToList();
                return JsonConvert.SerializeObject(animals);
                    }
            else
            {
                return null;
            }
          
        }
        [Authorize(Roles = "admin,vet")]
        public async Task<string> GetDisableTimes(DateTime? date, int? id)
        {
            if (date != null)
            {
                var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { Date = date, VetId = id  }));
                var test = inspections.Objects.Select(i => new List<string>() { i.Date.Value.ToShortTimeString(), new TimeSpan(i.Date.Value.TimeOfDay.Hours+1, i.Date.Value.TimeOfDay.Minutes, i.Date.Value.TimeOfDay.Seconds).ToString() });
                var td = JsonConvert.SerializeObject(test);
                return JsonConvert.SerializeObject(test);
            }
            else
            {
                return null;
            }

        }
        [Authorize(Roles = "admin,vet")]
        public async Task<string> GetDisableDates(DateTime? date, int? id)
        {
            var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() {  CurrentMonth = DateTime.Now, VetId=id })).Objects.Select(i=> i.Date).ToList();
            var dates = inspections.GroupBy(i => i.Value.Day);
            var td = dates.Select(i => new List<string>() { i.FirstOrDefault().Value.ToShortDateString(), i.Count().ToString() });
            var disableDates = td.Where(i => int.Parse(i[1]) == 9).Select(i => i[0]);
            return JsonConvert.SerializeObject(disableDates);

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
        public async Task<ActionResult> DetailsVet(int? id , DateTime? date)
        {
            var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { VetId = id , Date = date })).Objects.ToList();
            ViewBag.Treatments = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams() { VetId = id ,IsDischarged= true })).Objects.ToList();
            ViewBag.Date = date;
            return View(inspections);
        }
    }
}
