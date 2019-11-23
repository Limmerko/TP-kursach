using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;

namespace Computer_Store.DAO.DAOClasses
{
    public class OrderListDAO : DAO
    {
        public OrderListDAO()
        {
            connect();
        }
        public List<OrderList> getList(int basketId)
        {
            List<OrderList> orderList = new List<OrderList>();
            try
            {
                string sql = "SELECT*FROM Order_list where Id_Basket=" + basketId;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrderList order = new OrderList()
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        basketId = Convert.ToInt32(reader["Id_Basket"]),
                        productId = Convert.ToInt32(reader["Id_Product"]),
                        statusId = Convert.ToInt32(reader["Status"])
                    };
                    orderList.Add(order);
                }
                reader.Close();
                return orderList;
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
                return null;
            }
        }

        public void add(int productId, int basketId)
        {
            try
            {
                string sql = "INSERT INTO Order_list (Id_Basket, Id_Product, Status) VALUES (@1, @2, @3)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", basketId);
                cmd.Parameters.AddWithValue("@2", productId);
                cmd.Parameters.AddWithValue("@3", 4);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
        }
    }
}