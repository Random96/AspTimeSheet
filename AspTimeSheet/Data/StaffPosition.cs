using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Data
{
    /// <summary>
    /// Должности
    /// </summary>
    public class StaffPosition : IKeyable
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование должности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пропуски рабоников
        /// </summary>
        public virtual List<Hooky> Hooky { get; set; }
        /// <summary>
        /// Занятие должностей
        /// </summary>
        public virtual List<Staffing> Staffing { get; set; }
    }
}
