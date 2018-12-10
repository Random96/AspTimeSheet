using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Models
{
    public class HookyModelView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Не указана должность")]
        public int ? PositionId { get; set; }

        [Display(Name = "Должность")]
        public string PositionName { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Сотрудник")]
        [Required(ErrorMessage = "Не указан сотрудник")]
        public int ? PersonId { get; set; }

        [Display(Name = "Имя")]
        public string PersonName { get; set; }

        [Display(Name = "Отчество")]
        public string PersonMiddleName { get; set; }

        [Display(Name = "Фамилия")]
        public string PersonLastName { get; set; }

        [Display(Name = "Причина отстутствия")]
        public string Comment { get; set; }

        [Display(Name = "Дата отсутствия")]
        [Required(ErrorMessage = "Не указана дата отсутствия на рабочем месте")]
        [DataType(DataType.Date)]
        public DateTime ? HookyDate { get; set; }

        [Display(Name = "Время отстутствия")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Не указано время отсутствия на рабочем месте")]
        public TimeSpan ? HookyTime  { get; set; }
    }
}
