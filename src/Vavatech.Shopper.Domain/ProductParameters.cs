using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain
{
    public class PagingParameters
    {
        public int PageSize { get; set; } = 15;
        public int StartIndex { get; set; }
    }

    public class VirtualizeResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalSize { get; set; }
    }
}
