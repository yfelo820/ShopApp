using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max characters Client name is 50")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Email address is in an incorrect format")]
        [MaxLength(40, ErrorMessage = "Max characters Client email is 40")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "Max characters Client phonenumber is 20")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
