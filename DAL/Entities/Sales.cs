using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Sales
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "You only can request 10000 products per order")]
        public int Amount { get; set; }
    }
}
