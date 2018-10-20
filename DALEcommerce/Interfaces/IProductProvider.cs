using CommonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DALEcommerce.Interfaces
{
    public interface IProductProvider
    {
        int CreateProduct(Product product);
        void CreatePrice(int productId, double price);

        IEnumerable<ProductCategory> ReadAllProductCategories();
        IEnumerable<Product> ReadAllProducts();
        Product ReadProductById(int Id);

        void UpdateProduct(Product product);

        int DeleteProduct(int Id);
    }
}
