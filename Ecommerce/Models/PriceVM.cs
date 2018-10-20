using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CommonModels;

namespace Ecommerce.Models
{
    public class PriceVM 
    {
        public int ProductId { get; set; }

        [DisplayName("Price")]
        [Range(typeof(double),"0.1", "100000000000000000000", ErrorMessage ="Invalid price")]
        public double UnitPrice { get; set; }
    }
}
