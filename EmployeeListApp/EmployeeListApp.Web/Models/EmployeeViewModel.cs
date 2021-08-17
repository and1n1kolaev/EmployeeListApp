using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeListApp.WEB.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string SecondName { get; set; }
        [Display(Name = "Возраст")]
        [Range(18, 100)]
        [Required(ErrorMessage = "Обязательное поле")]
        public int Age { get; set; }
        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Обязательное поле")]
        public Gender Gender { get; set; }
        [Display(Name = "Наименование отдела")]
        public List<ItemView> Departments { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Этаж")]
        public int DepartmentFloor { get; set; }
        [Display(Name = "Языки")]
        public List<ItemView> Languages { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public List<Guid> LanguagesId { get; set; }

    }
}
