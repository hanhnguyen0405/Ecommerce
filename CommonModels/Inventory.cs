using System;

namespace CommonModels
{
    public class Inventory
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int LowThreshold { get; set; }
        public int HighThreshold { get; set; }
    }
}
