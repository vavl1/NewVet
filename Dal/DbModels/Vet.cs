using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Vet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string? FatherName { get; set; }

    public string? Phone { get; set; }

    public int? RoleType { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? PhotoParth { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; } = new List<Diagnosis>();

    public virtual ICollection<Inspection> Inspections { get; } = new List<Inspection>();
}
