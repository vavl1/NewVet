using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Treatment
{
    public int Id { get; set; }

    public int? VetId { get; set; }

    public int? AnimalId { get; set; }
    public bool? IsDischarged { get; set; }
    public DateTime? DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual ICollection<Inspection> Inspections { get; } = new List<Inspection>();

    public virtual Vet? Vet { get; set; }
}
