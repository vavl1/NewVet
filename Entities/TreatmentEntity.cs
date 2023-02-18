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

        public int? VetId { get; set; }

        public int? AnimalId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public virtual AnimalEntity? Animal { get; set; }

        public virtual VetEntity? Vet { get; set; }
    }
}
