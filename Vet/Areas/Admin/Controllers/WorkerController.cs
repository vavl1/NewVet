using Dal;
using Entities;
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
            ViewBag.VetId = id;
            //var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { VetId = id, Date = DateTime.Now.ToLocalTime() })).Objects.ToList();
              var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { VetId = id, Date = DateTime.Now.ToLocalTime() })).Objects.Where(i => i.Date.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            ViewBag.Treatments = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams() { VetId = id , IsDischarged=true})).Objects.ToList();
           
            return View(inspections);
        }
        public async Task<IActionResult> CanselTreatment(int? animalid, int? treatmentid, int? vetId)
        {
            var pet = await new AnimalDal().GetAsync(animalid.GetValueOrDefault());
            pet.VetId = null;
            await new AnimalDal().AddOrUpdateAsync(pet);
            var treatment = await new TreatmetsDal().GetAsync(treatmentid.GetValueOrDefault());
            treatment.DateEnd = DateTime.Now;
            treatment.IsDischarged = true;
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
        [HttpPost]
        public async Task<IActionResult> CreateInspection(InspectionEntity model)
        {
            if (ModelState.IsValid)
            {
                var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { Date = model.Date })).Objects.ToList();
                if (inspections.Count == 0)
                {
                    await new InspectionDal().AddOrUpdateAsync(model);
                }
            }
            ViewData["Error"] = "df";
             return Redirect("/Admin/Worker/Index/" + model.VetId);
            
        }
        public async Task<ActionResult> CanselInspection(int inspectionId, int vetId)
        {
            await new InspectionDal().DeleteAsync(inspectionId);
            return Redirect("/Admin/Worker/Index/" + vetId);
        }
    }
}
