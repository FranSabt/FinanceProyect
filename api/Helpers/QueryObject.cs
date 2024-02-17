using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? Sortby { get; set; } = null;
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageZise { get; set; } = 2;

    }
}