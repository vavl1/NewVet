using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SearchParams
{    public  class TreatmentSearchParams:BaseSearchParams
    {
        public int? VetId { get;set; }
        public int? AnimalId { get;set; }
    }
}
