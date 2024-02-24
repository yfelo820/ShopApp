using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class RegisterSaleBody
    {
        public int clientId { get; set; }
        public int productId { get; set; }
        public int amount { get; set; }
    }
}
