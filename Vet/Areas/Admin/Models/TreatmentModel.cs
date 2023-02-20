namespace Vet.Areas.Admin.Models
{
    public class TreatmentModel
    {
        public int? VetId { get; set; }

        public int? AnimalId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }
        public string? Diagnos { get; set; }
    }
}
