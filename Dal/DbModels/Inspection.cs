using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Inspection
{
    public int Id { get; set; }

    public int? VetId { get; set; }

    public int? AnimalId { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsOk { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual ICollection<Treatment> Treatments { get; } = new List<Treatment>();

    public virtual Vet? Vet { get; set; }
}
