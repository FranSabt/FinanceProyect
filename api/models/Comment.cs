using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? StockId { get; set; }

        // Navigation
        public Stock? Stock { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}