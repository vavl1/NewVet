using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities
{
    public class AnimalOwnerEntity
    {
        [Display(Name = "Ид")]
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
       
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
      
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        
        public string? FatherName { get; set; }
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }
        [Display(Name = "Адресс")]
        public string? Adress { get; set; }
        [Display(Name = "Логин")]
        public string? Login { get; set; }
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
        public virtual ICollection<AnimalEntity> Animals { get; set; } = new List<AnimalEntity>();
    }
}
