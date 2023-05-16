using Dal;
using Entities.SearchParams;
using Entities;
using Microsoft.AspNetCore.Mvc;
using VetAnimalOwnerLK.Models;

namespace VetAnimalOwnerLK.Areas.Public.Controllers
{
    [Area("Public")]
    public class TreatmentController : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 5;
            var treatment = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams() {  })).Objects.ToList();
            treatment = treatment.Where(i => i.Inspection?.Animal?.AnimalOwner == AnimalOwnerParams.Id).ToList();
            var count = treatment.Count;
            var items = treatment.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(new PageModel<TreatmentEntity>(count, page, pageSize, items));
        }
    }
}
