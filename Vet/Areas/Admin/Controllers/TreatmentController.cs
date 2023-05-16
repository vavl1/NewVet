using ClosedXML.Excel;
using Dal;
using Dal.DbModels;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Mvc;
using Vet.Areas.Admin.Models;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TreatmentController : Controller
    {
        IWebHostEnvironment test;
        public TreatmentController(IWebHostEnvironment test)
        {
            this.test = test;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 5;
            var treatment = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams() { })).Objects.Select(i=> new TreatmentEntity
            {
                Description = i.Description,
                IsDischarged = i.IsDischarged,
                Id = i.Id,
                Inspection = new InspectionEntity()
                {
                    Animal = new AnimalDal().GetAsync((i.Inspection?.AnimalId.GetValueOrDefault()).GetValueOrDefault()).Result,
                    Vet = new VetsDal().GetAsync((i.Inspection?.VetId.GetValueOrDefault()).GetValueOrDefault()).Result
                },
                
            } ).ToList();
            var count = treatment.Count;
            var items = treatment.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(new PageModel<TreatmentEntity>(count,page,pageSize,items));
        }
        [HttpPost]
        public async Task DownLoad(List<TreatmentEntity>? treatments)
        {
            var name = $"data_{DateTime.Now.ToShortDateString()}.xlsx";
            var parth = "/images/" + name;
            using (var XMLBook = new XLWorkbook())
            {
                
               
                var workSheet = XMLBook.Worksheets.Add("Treatments");
                workSheet.Cell("A1").Value = "Питомец";
                workSheet.Cell("B1").Value = "Лечащий врач";
                workSheet.Cell("C1").Value = "Описание";
               
                for (int i = 0; i < treatments.Count; i++)
                {
                    workSheet.Cell(i + 2, 1).Value = treatments[i].Inspection?.Animal?.NickName;
                    workSheet.Cell(i + 2, 2).Value = treatments[i].Inspection?.Vet?.Name + " " + treatments[i].Inspection?.Vet?.FatherName;
                    workSheet.Cell(i + 2, 3).Value = treatments[i].Description;
                   
                }
               
                    XMLBook.SaveAs(test.WebRootPath + parth);
               

            }
           
        }
        public async Task<IActionResult> DownLoad()
        {
            var name = $"data_{DateTime.Now.ToShortDateString()}.xlsx";
            var parth = "/images/" + name;
           
            var fs = new FileStream(test.WebRootPath + parth, FileMode.Open);
            var td = File(fs, "application/octet-stream", name);
            return td;
        }

    }
}
