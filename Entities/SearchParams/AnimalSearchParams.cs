using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.SearchParams
{
    public class AnimalSearchParams:BaseSearchParams
    {
        public int? AnimalOwnerId { get; set; }
    }
}
