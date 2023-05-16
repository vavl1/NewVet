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
        [Display(Name = "Диагноз")]
       
        public string? Diagnos { get; set; }

        [Display(Name = "Предписание по лечению")]
        public string? Description { get; set; }
       
        public int? InspectionId { get; set; }
    }
}

