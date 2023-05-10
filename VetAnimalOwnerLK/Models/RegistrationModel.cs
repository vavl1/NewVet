using System.ComponentModel.DataAnnotations;

namespace VetAnimalOwnerLK.Models
{
    public class RegistrationModel
    {
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string FatherName { get; set; }
    }
}
