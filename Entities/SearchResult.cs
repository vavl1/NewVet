using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SearchResult<TEntity> where TEntity : class
    {
        public SearchResult()
        {

        }
        public int Total { get; set; }
        public int? RequestedObjectsCount { get; set; }
        public int RequestedStartIndex { get; set; }
        public IList<TEntity> Objects { get; set; }
    }
}
