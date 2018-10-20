using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItem> ProductItems { get; set; }
    }
}
