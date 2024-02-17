using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class UpdateRequestDto
    {
        [MaxLength(10, ErrorMessage = "Max 10 charachters for Symbol.")]
        public string Symbol { get; set; } = string.Empty;

        [MaxLength(15, ErrorMessage = "Max 10 charachters for CompanyName.")]
        public string CompanyName { get; set; } = string.Empty;

        [Range(1, 1_000_000_000)]
        public decimal Money { get; set; }

        [Range(0.001, 1_000_000)]
        public decimal LastDiv { get; set; }

        [MaxLength(10, ErrorMessage = "Max 10 charachters for Industry.")]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 1_000_000)]
        public long MarketCap {get; set; }
    }
}