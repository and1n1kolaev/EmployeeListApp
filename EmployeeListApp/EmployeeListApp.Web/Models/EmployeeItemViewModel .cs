using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeListApp.WEB.Models
{
    public class EmployeeItemViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }
        public string StringGender 
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Male: return "Мужской";
                    case Gender.Female: return "Женский";
                    default: return "";
                }
            }
        }
        [Display(Name = "Наименование отдела")]
        public string DepartmentName { get; set; }
        [Display(Name = "Этаж")]
        public int DepartmentFloor { get; set; }
        [Display(Name = "Опыт")]
        public List<string> Experience { get; set; }

        public EmployeeItemViewModel()
        {
            Experience = new List<string>();
        }

    }
}
