using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TreatmentEntity
    {
        public int Id { get; set; }

      
        public bool? IsDischarged { get; set; }

        public int? InspectionId { get; set; }

        public string? Description { get; set; }

        public virtual InspectionEntity? Inspection { get; set; }
    }
}
