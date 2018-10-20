using CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLLEcommerce.Interfaces
{
    public interface IBLL_Product
    {
        void CreatNewProduct(Product product);
        Product GetProductById(int productId);

        IEnumerable<ProductCategory> GetAllProductCategories();
        IEnumerable<Product> GetAllProducts();
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
       
    }
}
