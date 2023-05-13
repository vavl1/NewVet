using Dal;
using Entities.SearchParams;
using Entities;
using Microsoft.AspNetCore.Mvc;
using VetAnimalOwnerLK.Models;

namespace VetAnimalOwnerLK.Areas.Public.Controllers
{
    public class InspectionController : Controller
    {
        [Area("Public")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 5;
            var treatment = (await new InspectionDal().GetAsync(new InspectionSearchParams() { })).Objects.ToList();
            treatment = treatment.Where(i => i.Animal?.AnimalOwner == AnimalOwnerParams.Id).Select( i=> new InspectionEntity()
            {
Date = i.Date,
Description =i.Description,
Animal = i.Animal,
Treatment = i.Treatment,
Vet = new VetsDal().GetAsync(i.VetId.GetValueOrDefault()).Result,

            }).ToList();
             var count = treatment.Count;
            var items = treatment.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(new PageModel<InspectionEntity>(count, page, pageSize, items));
        }
    }
}
