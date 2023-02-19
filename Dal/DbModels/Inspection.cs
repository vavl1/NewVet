using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Inspection
{
    public int Id { get; set; }

    public int? VetId { get; set; }

    public int? AnimalId { get; set; }

    public int? TreatmentId { get; set; }

    public string? Description { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Treatment? Treatment { get; set; }

    public virtual Vet? Vet { get; set; }
}
