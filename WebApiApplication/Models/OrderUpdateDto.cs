using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Models
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
    }
}
