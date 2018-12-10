using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspTimeSheet.Data
{
    public interface IKeyable
    {
        int Id { get; set; }
    }
}
