namespace Vet.Areas.Admin.Models
{
    public class AnimalOwnerModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? LastName { get; set; }

        public string? FatherName { get; set; }

        public string? Phone { get; set; }

        public string? Adress { get; set; }
        public int? CountAnimals { get; set; }
    }
}
