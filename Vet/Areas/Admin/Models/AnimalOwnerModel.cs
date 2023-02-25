using System.ComponentModel.DataAnnotations;

namespace Vet.Areas.Admin.Models
{
    public class AnimalOwnerModel
    {
        public int Id { get; set; }
        [Display(Name ="Имя")]
        public string Name { get; set; } = null!;
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }
        [Display(Name = "Отчество")]
        public string? FatherName { get; set; }
        [Display(Name = "Телеыон")]
        public string? Phone { get; set; }
        [Display(Name = "Адресс")]
        public string? Adress { get; set; }
        public int? CountAnimals { get; set; }
    }
}
