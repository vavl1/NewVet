using Dal;
using Entities.SearchParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "vet")]
    public class WorkerController : Controller
    {
        public async Task<IActionResult> Index(int? id)
        {
            var inspections = (await new InspectionDal().GetAsync(new InspectionSearchParams() { VetId = id })).Objects.ToList();
            ViewBag.Treatments = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams() { VetId = id })).Objects.ToList();
           
            return View(inspections);
        }
    }
}
