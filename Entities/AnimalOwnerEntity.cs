using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AnimalOwnerEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public string? Phone { get; set; }

        public string? Adress { get; set; }

        public virtual ICollection<AnimalEntity> Animals { get; set; } = new List<AnimalEntity>();
    }
}
