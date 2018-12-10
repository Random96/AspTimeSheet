using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Data
{
    /// <summary>
    /// Работники
    /// </summary>
    public class Person : IKeyable
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Табельный номер
        /// </summary>
        public string PersonnelNumber { get; set; }

        /// <summary>
        /// Занимаемые должности
        /// </summary>
        public virtual List<Staffing> Stuffing { get; set; }
        
        /// <summary>
        /// Отсутствие на рабочем месте
        /// </summary>
        public virtual List<Hooky> Hookeys { get; set; }
    }
}
