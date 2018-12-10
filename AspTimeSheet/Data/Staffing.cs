using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Data
{
    /// <summary>
    /// Штатное расписание
    /// </summary>
    public class Staffing : IKeyable
    {
        public int Id { get; set; }

        /// <summary>
        /// Рабоник
        /// </summary>
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public int PositionId { get; set; }
        public virtual StaffPosition Position { get; set; }

        /// <summary>
        /// Дата занятия должности
        /// </summary>
        public virtual DateTime FromDate { get; set; }
        /// <summary>
        /// Дата освобождения от должности
        /// </summary>
        public virtual DateTime? ToDate { get; set; }
    }
}
