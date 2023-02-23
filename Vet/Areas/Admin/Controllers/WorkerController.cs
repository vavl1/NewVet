using Dal;
using Entities.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Vet.Areas.Admin.Models;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "vet")]
    public class WorkerController : Controller
    {
        public async Task<IActionResult> Index(int? id)
        {
            var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { VetId = id,Date = DateTime.Now })).Objects.ToList();
            ViewBag.Treatments = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams() { VetId = id })).Objects.ToList();
           
            return View(inspections);
        }
        public async Task<IActionResult> CanselTreatment(int? animalid, int? treatmentid, int? vetId)
        {
            var pet = await new AnimalDal().GetAsync(animalid.GetValueOrDefault());
            pet.VetId = null;
            await new AnimalDal().AddOrUpdateAsync(pet);
            var treatment = await new TreatmetsDal().GetAsync(treatmentid.GetValueOrDefault());
            treatment.DateEnd = DateTime.Now;
            await new TreatmetsDal().AddOrUpdateAsync(treatment);

           
            return Redirect("/Admin/Worker/Index/" +vetId);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTreatment(TreatmentModel model)
        {
            if (ModelState.IsValid)
            {
                var animal = await new AnimalDal().GetAsync(model.AnimalId.GetValueOrDefault());
                animal.VetId = model.VetId;
                var diagnosId = await new DiagnosisDal().AddOrUpdateAsync(new Entities.DiagnosisEntity()
                {
                    AnimalId = model.AnimalId,
                    VetId = model.VetId,
                    Date = DateTime.Now,
                    Name = model.Diagnos,
                });
                var treatment = await new TreatmetsDal().AddOrUpdateAsync(new Entities.TreatmentEntity
                {
                    AnimalId = model.AnimalId,
                    VetId = model.VetId,
                    DateStart = model.DateStart,
                    DateEnd = model.DateEnd
                });
                await new AnimalDal().AddOrUpdateAsync(animal);
            }
           
            return Redirect("/Admin/Worker/Index/"+model.VetId);
        }
    }
}
