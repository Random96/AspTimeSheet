using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Models
{
    public class StaffPositionModelView
    {
        public int Id { get; set; }

        [Display(Name = "Выбор")]
        public bool Checked { get; set; }

        [Required(ErrorMessage = "Не указано наименование должности")]
        [Display(Name = "Должность")]
        public string Name { get; set; }
    }
}
