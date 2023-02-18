using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class AnimalOwner
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LastName { get; set; }

    public string? FatherName { get; set; }

    public string? Phone { get; set; }

    public string? Adress { get; set; }

    public virtual ICollection<Animal> Animals { get; } = new List<Animal>();
}
