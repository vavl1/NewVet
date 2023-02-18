using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Diagnosis
{
    public int Id { get; set; }

    public int? VetId { get; set; }

    public int? AnimalId { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public virtual Animal? Animal { get; set; }

    public virtual Vet? Vet { get; set; }
}
