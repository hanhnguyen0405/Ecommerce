using CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLLEcommerce.Interfaces
{
    public interface IBLL_Product
    {
        void CreatNewProduct(Product product);
        Product GetProductById(int id);

        IEnumerable<ProductCategory> GetAllProductCategories();
        IEnumerable<Product> GetAllProducts();
        int UpdateProductById(Product product);
       
    }
}
