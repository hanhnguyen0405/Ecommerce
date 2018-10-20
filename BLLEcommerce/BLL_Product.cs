using System;
using System.Collections.Generic;
using DALEcommerce;
using CommonModels;
using DALEcommerce.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLLEcommerce
{
    public class BLL_Product : Interfaces.IBLL_Product
    {
        private readonly string _connectionString;
        private IProductProvider _productProvider;

        public BLL_Product(IConfiguration iconfiguration)
        {
            _productProvider = new ProductProvider(iconfiguration);
        }

        public void CreatNewProduct(Product product)
        {
            int productId= _productProvider.CreateProduct(product);
            _productProvider.CreatePrice(productId, product.Price.UnitPrice);
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            List<ProductCategory> results = new List<ProductCategory>();
            results =  _productProvider.ReadAllProductCategories() as List<ProductCategory>;
            return results;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> results = new List<Product>();
            results = _productProvider.ReadAllProducts() as List<Product>;
            return results;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateProductById(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
