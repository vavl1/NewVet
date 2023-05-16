using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AnimalEntity
    {
        public int Id { get; set; }

        public int? VetId { get; set; }

        public int? AnimalOwner { get; set; }

        public string? NickName { get; set; }

        public DateTime? Birthay { get; set; }

        public bool? Gender { get; set; }

        public string? Breed { get; set; }
        public string? PhotoParth { get; set; }
        public bool? IsHealthy { get; set; }

        public virtual AnimalOwnerEntity? AnimalOwnerNavigation { get; set; }

        public virtual ICollection<DiagnosisEntity> Diagnoses { get; set; } = new List<DiagnosisEntity>();

        public virtual ICollection<TreatmentEntity> Treatments { get; set; } = new List<TreatmentEntity>();

        public virtual VetEntity? Vet { get; set; }
    }
}
