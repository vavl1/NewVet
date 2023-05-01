using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SearchParams
{
    public class InspectionSearchParams:BaseSearchParams
    {
        public int? VetId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CurrentMonth { get; set; } 
       
    }
}
