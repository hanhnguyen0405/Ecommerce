using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ecommerce
{
    public class ProductCategoryVM
    {
        public int Id { get; set; }

        [DisplayName("Product Category")]
        public string Name { get; set; }
    }
}
