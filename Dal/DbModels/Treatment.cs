using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Treatment
{
    public int Id { get; set; }

    public bool? IsDischarged { get; set; }

    public int? InspectionId { get; set; }

    public string? Description { get; set; }

    public virtual Inspection? Inspection { get; set; }
}
