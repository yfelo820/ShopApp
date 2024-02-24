using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SalesResponse
    {
        public string ClientName { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
