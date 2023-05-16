using Dal;
using Dal.DbModels;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace VetAnimalOwnerLK.Areas.Public.Controllers
{
    [Area("Public")]
    public class HomeController : Controller
    {
        IWebHostEnvironment test;
        public HomeController(IWebHostEnvironment test)
        {
            this.test = test;
        }
        public async Task<IActionResult> Index()
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
          
            var pets = (await new AnimalDal().GetAsync(new AnimalSearchParams() { AnimalOwnerId = AnimalOwnerParams.Id })).Objects.ToList();
            var vets = (await new VetsDal().GetAsync(new VetSearchParams() { Role = RoleType.vet })).Objects.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.LastName + " " + i.Name + " " + i.FatherName }).ToList();
            ViewBag.Vets = vets;
            return View(pets);
        }
        [HttpPost]
        public async Task<IActionResult> AddAnimal(AnimalEntity animals)
        {
            if (ModelState.IsValid)
            {
                
                var id = await new AnimalDal().AddOrUpdateAsync(animals);
                var animal = await new AnimalDal().GetAsync(id);

            }
            return Redirect("/Public/Home/Index");
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
        [HttpPost]
        public async Task<string> GetAnimal(int? id)
        {
            var animal = await new AnimalDal().GetAsync(id.GetValueOrDefault());
            return JsonConvert.SerializeObject(animal);



        }
        public async Task<ActionResult> DeleteAnimal(int? id)
        {
            if (id != null)
            {

                var animal = await new AnimalDal().DeleteAsync(id.GetValueOrDefault());


            }
            return Redirect("/Public/Home/Index");


        }
        public async Task<string> GetDisableDates(int? id)
        {
            var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { CurrentMonth = DateTime.Now, VetId = id })).Objects.Select(i => i.Date).ToList();
            var disableDates = GetDisableDates(inspections).Result;
            return JsonConvert.SerializeObject(disableDates);

        }
        private async Task<IEnumerable<string>> GetDisableDates(List<DateTime?>? inspections)
        {
            var dates = inspections.GroupBy(i => i.Value.Day);
            var td = dates.Select(i => new List<string>() { i.FirstOrDefault().Value.ToShortDateString(), i.Count().ToString() });
            var disableDates = td.Where(i => int.Parse(i[1]) == 9).Select(i => i[0]).ToList();
            return disableDates;
        }
        public async Task<IActionResult> CreateInspection(InspectionEntity model, TimeSpan Time)
        {
            if (ModelState.IsValid)
            {

                model.Date = new DateTime(model.Date.Value.Year, model.Date.Value.Month, model.Date.Value.Day, Time.Hours, Time.Minutes, Time.Seconds);
                model.IsOk = false;
                await new InspectionDal().AddOrUpdateAsync(model);
            }
            return Redirect("/Public/Home/Index");

        }
        public async Task<string> GetDisableTimes(DateTime? date, int? id)
        {
            if (date != null)
            {
                var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { Date = date, VetId = id })).Objects;
                var test = inspections.Select(i => new List<string>() { i.Date.Value.ToShortTimeString(), new TimeSpan(i.Date.Value.TimeOfDay.Hours + 1, i.Date.Value.TimeOfDay.Minutes, i.Date.Value.TimeOfDay.Seconds).ToString() });
                var td = JsonConvert.SerializeObject(test);
                return JsonConvert.SerializeObject(test);
            }
            else
            {
                return null;
            }

        }
    }
}
