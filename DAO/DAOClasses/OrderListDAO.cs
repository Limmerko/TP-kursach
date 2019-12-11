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
        public List<OrderList> getList(int basketId)
        {
            connect();
            List<OrderList> orderList = new List<OrderList>();
            try
            {
                Logger.log.Info("Выполнение запроса на получение списка заказов из корзины с Id = " + basketId);
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
            finally
            {
                disconnect();
            }
        }

        public void add(int productId, int basketId)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на добавление товара с Id = "+ productId + " в список заказов из корзины с Id = " + basketId);
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
            finally
            {
                disconnect();
            }
        }

        public void order(int basketId)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на заказ товаров из списка заказов из корзины с Id = " + basketId);
                string sql = "UPDATE Order_list SET Status=" + 3 + " where Id_Basket=" + basketId+"and Status="+4;
                new SqlCommand(sql, connection).ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
            finally
            {
                disconnect();
            }
        }

        public void delete(int productId, int basketId)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на удаление товара с Id из списка заказов корзины с Id = " + basketId);
                string sql = "DELETE FROM Order_list where Id_Basket=" + basketId+ " and Id_Product = "+ productId;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
            finally
            {
                disconnect();
            }
        }

        public void deliveredUpdate(int productId, int basketId)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на обновление статуса товара в списке заказов у коризны с Id = " + basketId);
                string sql = "UPDATE Order_list SET Status="+ 5 + " where Id_Basket=" + basketId + " and Id_Product = "+ productId;
                new SqlCommand(sql, connection).ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
            finally
            {
                disconnect();
            }
        }
    }
}