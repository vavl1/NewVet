using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SearchParams
{
   public class VetSearchParams:BaseSearchParams
    {
        public string? Login { get; set; }
        public RoleType? Role { get; set; }
        public DateTime? Date { get; set; }
        public  string? Name { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }

    }
}
