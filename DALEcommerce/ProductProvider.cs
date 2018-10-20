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
        public void CreateProduct(Product product)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spProductCreate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@ProductCategoryId", product.ProductCategory.Id);
                        cmd.Parameters.AddWithValue("@UnitPrice", product.Price.UnitPrice);
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

        public void CreatePrice(int productId, double price)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spPriceCreate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
                    using (SqlCommand cmd = new SqlCommand("spProductCategoryReadAll", conn))
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
                    using (SqlCommand cmd = new SqlCommand("spProductReadAll", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            conn.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {

                                while (rdr.Read())
                                {
                                    Product p = new Product();
                                    p.Id = Convert.ToInt32(rdr["Id"]);
                                    p.Name = rdr["ProductName"].ToString();
                                    p.Description = rdr["Description"].ToString();
                                    p.Price = new Price
                                    {
                                        ProductId = Convert.ToInt32(rdr["Id"]),
                                        UnitPrice = Convert.ToDouble(rdr["UnitPrice"])
                                    };
                                    p.ProductCategory = new ProductCategory
                                    {
                                        Id = Convert.ToInt32(rdr["ProductCategoryID"]),
                                        Name = rdr["ProductCategoryName"].ToString()
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

        public Product ReadProductByProductId(int productId)
        {
            Product p = new Product();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spProductReadByProductId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        try
                        {
                            conn.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {

                                if (rdr.Read())
                                {
                                    p.Id = Convert.ToInt32(rdr["Id"]);
                                    p.Name = rdr["ProductName"].ToString();
                                    p.Description = rdr["Description"].ToString();
                                    p.Price = new Price
                                    {
                                        ProductId = Convert.ToInt32(rdr["Id"]),
                                        UnitPrice = Convert.ToDouble(rdr["UnitPrice"])
                                    };
                                    p.ProductCategory = new ProductCategory
                                    {
                                        Id = Convert.ToInt32(rdr["ProductCategoryID"]),
                                        Name = rdr["ProductCategoryName"].ToString()
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
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spProductUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", product.Id);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.Parameters.AddWithValue("@ProductCategoryId", product.ProductCategory.Id);
                        cmd.Parameters.AddWithValue("@UnitPrice", product.Price.UnitPrice);

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

        //====================================DELETE===================================//

        public void DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spProductDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productId);
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
    }
}
