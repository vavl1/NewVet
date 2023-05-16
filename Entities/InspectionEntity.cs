using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InspectionEntity
    {
        public int Id { get; set; }

        public int? VetId { get; set; }

        public int? AnimalId { get; set; }

        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsOk { get; set; }
        public virtual AnimalEntity? Animal { get; set; }

        public virtual ICollection<TreatmentEntity> Treatments { get; } = new List<TreatmentEntity>();

        public virtual VetEntity? Vet { get; set; }
    }
}
