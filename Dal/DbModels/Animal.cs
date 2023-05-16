using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Animal
{
    public int Id { get; set; }

    public int? AnimalOwner { get; set; }

    public string? NickName { get; set; }

    public DateTime? Birthay { get; set; }

    public bool? Gender { get; set; }

    public string? Breed { get; set; }

    public string? PhotoParth { get; set; }

    public bool? IsHealthy { get; set; }

    public virtual AnimalOwner? AnimalOwnerNavigation { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; } = new List<Diagnosis>();

    public virtual ICollection<Inspection> Inspections { get; } = new List<Inspection>();
}
