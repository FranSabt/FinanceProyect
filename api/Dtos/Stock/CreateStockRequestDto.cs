using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Max 10 charachters for Symbol.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(15, ErrorMessage = "Max 10 charachters for CompanyName.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1_000_000_000)]
        public decimal Money { get; set; }
        [Required]
        [Range(0.001, 1_000_000)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Max 10 charachters for Industry.")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 1_000_000)]
        public long MarketCap {get; set; }
    }
}