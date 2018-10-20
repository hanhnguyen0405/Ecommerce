using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommonModels;

namespace Ecommerce.Models
{
    public class ProductVM 
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [DisplayName("Product Description")]
        public string Description { get; set; }

        [DisplayName("Product Category")]
        public ProductCategoryVM ProductCategory { get; set; }

        public PriceVM Price { get; set; }
    }
}
