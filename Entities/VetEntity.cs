using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class VetEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public string? Phone { get; set; }

        public RoleType? RoleType { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? PhotoParth { get; set; }

        public virtual ICollection<AnimalEntity> Animals { get; } = new List<AnimalEntity>();

        public virtual ICollection<DiagnosisEntity> Diagnoses { get; } = new List<DiagnosisEntity>();

        public virtual ICollection<TreatmentEntity> Treatments { get; set; } = new List<TreatmentEntity>();
    }
}
