using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace Vet.Areas.Admin.Models
{
    public class TreatmentModel
    {
        [Display(Name = "Ветеринар")]
        public int? VetId { get; set; }
        [Display(Name = "Питомец")]
        public int? AnimalId { get; set; }
        [Display(Name = "Дата начала")]
        public DateTime? DateStart { get; set; }
        [Display(Name = "Дата конца")]
        public DateTime? DateEnd { get; set; }
        [Display(Name = "Диагноз")]
        public string? Diagnos { get; set; }
    }
}
