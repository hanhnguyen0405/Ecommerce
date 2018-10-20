using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonModels;
using Microsoft.Extensions.Configuration;

namespace DALEcommerce
{
    public class OrderProvider : Interfaces.IOrderProvider
    {
        private readonly string _connectionString;
        private DAL_Utilities _dalUtilities;

        public OrderProvider(IConfiguration iconfiguration)
        {
            _dalUtilities = new DAL_Utilities(iconfiguration);
            _connectionString = _dalUtilities.GetConnectionString();
        }

        public void AddItemIntoOrder(OrderItem item)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string sqlStatement = "Insert Into OrderItems (ProductId, Quantity, UnitPrice, OrderId)" +
                                            " Values (@ProductId, @Quantity, @UnitPrice, @OrderId)";
                    using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        cmd.Parameters.AddWithValue("@OrderId", item.OrderId);
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

        public int CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public int DeleteOrder(int Id)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentOrderId()
        {
            int orderId;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spOrderGetCurrentOrCreate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            conn.Open();
                            using (SqlDataReader rdr = cmd.ExecuteReader())
                            {
                                if (rdr.Read())
                                {
                                    orderId = Convert.ToInt32(rdr["Id"]);
                                }
                                else { throw new Exception("order Id not found... weird..."); }
                            }
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
            return orderId;
        }

        public Order ReadOrder(int Id)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            Order order = new Order();
            // Get the Order first
            string readOrderStatement = "Select Top 1 * From Orders o Where o.OrderStatus = 'Current'";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(readOrderStatement, conn))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlDataReader myReader = cmd.ExecuteReader())
                            {
                                while (myReader.Read())
                                {
                                    order.Id = Convert.ToInt32(myReader["Id"]);
                                    order.OrderDate = (DateTime)myReader["OrderDate"];
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
            catch (Exception e)
            {
                throw;
            }
            // Get the Order first
            string readOrderItemsStatement = "Select * From OrderItems oi Where oi.OrderId = @orderId";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(readOrderItemsStatement, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", order.Id);
                        try
                        {
                            conn.Open();
                            using (SqlDataReader myReader = cmd.ExecuteReader())
                            {
                                while (myReader.Read())
                                {
                                    OrderItem thisItem = new OrderItem()
                                    {
                                        OrderId = Convert.ToInt32(myReader["OrderId"]),
                                        ProductId = myReader["ProductId"].ToString(),
                                        Quantity = Convert.ToInt32(myReader["Quantity"]),
                                        UnitPrice = (float)myReader["UnitPrice"]
                                    };
                                    orderItems.Add(thisItem);
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
            catch (Exception e)
            {
                throw;
            }

            order.ProductItems = orderItems;
            return order;
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }


    }
}
