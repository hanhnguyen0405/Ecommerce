using CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DALEcommerce.Interfaces
{
    public interface IProductProvider
    {
        void CreateProduct(Product product);
        void CreatePrice(int productId, double price);

        IEnumerable<ProductCategory> ReadAllProductCategories();
        IEnumerable<Product> ReadAllProducts();
        Product ReadProductByProductId(int productId);

        void UpdateProduct(Product product);

        void DeleteProduct(int productId);
    }
}
