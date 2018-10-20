﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
    }
}
