using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class VetEntity
    {
        [Display(Name="Ид")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
    
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Введите Фамилию")]
        [Display(Name = "Фамилия")]
      
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
       
        public string? FatherName { get; set; }
        [Display(Name = "Телефон")]
        public string? Phone { get; set; }
        [Display(Name = "Роль")]
        public RoleType? RoleType { get; set; }
        [Required(ErrorMessage = "Введите Логин")]
        [Display(Name = "Логин")]
      
        public string? Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        
        public string? Password { get; set; }
        [Display(Name = "Изображение")]
        public string? PhotoParth { get; set; }

        public virtual ICollection<AnimalEntity> Animals { get; } = new List<AnimalEntity>();

        public virtual ICollection<DiagnosisEntity> Diagnoses { get; } = new List<DiagnosisEntity>();

        public virtual ICollection<TreatmentEntity> Treatments { get; set; } = new List<TreatmentEntity>();
        public virtual ICollection<InspectionEntity> Inspections { get; set; } = new List<InspectionEntity>();
    }
}
