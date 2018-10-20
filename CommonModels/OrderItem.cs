using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string OrderId { get; set; }
    }
}
