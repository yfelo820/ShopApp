using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Max characters Product name is 30")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "Max characters Product description is 50")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Precision(7,2)]
        [RegularExpression(@"^(0|[1-9]\d{0,4})(\.\d{1,2})?$", ErrorMessage = "The price must have 5 digits in the integer part and 2 in the decimal part")]
        public decimal Price { get; set; }

        [RegularExpression(@"^(0|[1-9]\d{0,6})?$", ErrorMessage = "The maximum amount received is 1 Million")]
        public int Stock { get; set; }
    }
}