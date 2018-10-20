using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonModels;

namespace DALEcommerce
{
    public class ProductProvider : Interfaces.IProductProvider
    {
        private readonly string _connectionString;
        private DAL_Utilities _dalUtilities;

        public ProductProvider(string ConnectionString)
        {
            this._connectionString = ConnectionString;
            _dalUtilities = new DAL_Utilities(_connectionString);
        }

        //====================================CREATE===================================//
        public int CreateProduct(Product product)
        {
            int productId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Insert into Product values (@name,@description,@product_category_id); SELECT CAST(scope_identity() AS int)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", product.Name);
                        cmd.Parameters.AddWithValue("@description", product.Description);
                        cmd.Parameters.AddWithValue("@product_category_id", product.ProductCategory.Id);
                        try
                        {
                            conn.Open();
                            productId = (int)cmd.ExecuteScalar();
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
                return productId;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void CreatePrice(int productId, double price)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Insert into Price values (@productId,@price)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@price", price);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //====================================READ===================================//
        public IEnumerable<ProductCategory> ReadAllProductCategories()
        {
            List<ProductCategory> results = new List<ProductCategory>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from ProductCategory", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        try
                        {
                            conn.Open();
                            using (SqlDataReader myReader = cmd.ExecuteReader())
                            {
                                while (myReader.Read())
                                {
                                    ProductCategory pc = new ProductCategory();
                                    pc.Id = myReader.GetInt32(0);
                                    pc.Name = myReader.GetString(1);
                                    results.Add(pc);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            List<Product> results = new List<Product>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Product,Price where Product.Id=Price.ProductId", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        try
                        {
                            conn.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {

                                while (rdr.Read())
                                {
                                    Product p = new Product();
                                    p.Id = Convert.ToInt32(rdr["Id"]);
                                    p.Name = rdr["Name"].ToString();
                                    p.Description = rdr["Description"].ToString();
                                    p.Price = new Price
                                    {
                                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                                        UnitPrice = Convert.ToDouble(rdr["UnitPrice"])
                                    };

                                    results.Add(p);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Product ReadProductById(int productId)
        {
            Product p = new Product();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Product,Price,ProductCategory where Product.Id=Price.ProductId and Product.Id=ProductCategory.ProductId and Product.Id=@Id", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", productId);
                        try
                        {
                            conn.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {

                                if (rdr.Read())
                                {
                                    p.Id = Convert.ToInt32(rdr["Id"]);
                                    p.Name = rdr["Name"].ToString();
                                    p.Description = rdr["Description"].ToString();
                                    p.Price = new Price
                                    {
                                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                                        UnitPrice = Convert.ToDouble(rdr["UnitPrice"])
                                    };
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }
                }
                return p;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //====================================UPDATE===================================//
        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        //====================================DELETE===================================//

        public int DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
