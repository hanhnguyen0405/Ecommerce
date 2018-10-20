using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DALEcommerce
{
    class DAL_Utilities
    {
        private readonly string _connectionString;

        public DAL_Utilities(string ConnectionString)
        {
            this._connectionString = ConnectionString;
        }

        //public string GetConnectionString()
        //{
        //    return _connectionString;
        //}

        //public int InsertUpdateDeleteQry(string sql)
        //{
        //    int rowsAffected = 0;
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                cmd.CommandText = sql;
        //                try
        //                {
        //                    conn.Open();
        //                    rowsAffected = cmd.ExecuteNonQuery();
        //                }
        //                catch (Exception e)
        //                {
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //    return rowsAffected;
        //}
    }
}
