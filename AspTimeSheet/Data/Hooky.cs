using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Data
{
    /// <summary>
    /// Отсутствие на рабочем месте
    /// </summary>
    public class Hooky : IKeyable
    {
        public int Id { get; set; }

        /// <summary>
        /// Работник
        /// </summary>
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        /// <summary>
        /// Занимаемая должность
        /// </summary>
        public int PositionId { get; set; }
        public virtual StaffPosition Position { get; set; }

        /// <summary>
        /// Дата начала отсутствия
        /// </summary>
        public virtual DateTime HookyDate {get; set;}
        /// <summary>
        /// Дата окончания отсутствия
        /// </summary>
        public virtual TimeSpan HookyTime { get; set; }

        /// <summary>
        /// приична
        /// </summary>
        public string Comment { get; set; }
    }
}
