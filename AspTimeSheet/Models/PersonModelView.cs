using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Models
{
    public class PersonModelView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Табельный номер")]
        [Required(ErrorMessage = "Не указан табельный номер")]
        public string PersonnelNumber { get; set; }

    }
}
