using ClosedXML.Excel;
using Dal;
using Dal.DbModels;
using Entities;
using Entities.SearchParams;
using Microsoft.AspNetCore.Mvc;

namespace Vet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TreatmentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var treatment = (await new TreatmetsDal().GetAsync(new TreatmentSearchParams())).Objects.ToList();
            return View(treatment);
        }
        [HttpPost]
        public async Task<IActionResult> DownLoad(List<TreatmentEntity>? treatments)
        {
            using (var XMLBook = new XLWorkbook())
            {
                var workSheet = XMLBook.Worksheets.Add("Treatments");
                workSheet.Cell("A1").Value = "Питомец";
                workSheet.Cell("B1").Value = "Лечащий врач";
                workSheet.Cell("C1").Value = "Дата начала";
                workSheet.Cell("D1").Value = "Дата конца";
                for (int i = 0; i < treatments.Count; i++)
                {
                    workSheet.Cell(i + 2, 1).Value = treatments[i].Animal?.NickName;
                    workSheet.Cell(i + 2, 2).Value = treatments[i].Vet?.Name + " " + treatments[i].Vet?.FatherName;
                    workSheet.Cell(i + 2, 3).Value = treatments[i].DateStart;
                    workSheet.Cell(i + 2, 4).Value = treatments[i].DateEnd;
                }
                using (var stream = new MemoryStream())
                {
                    XMLBook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Treatment_{DateTime.Now.ToShortTimeString()}.xlsx"
                    };
                }
            }
        }      
        
    }
}
