using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;

namespace Computer_Store.DAO.DAOClasses
{
    public class ShoppingListDAO : DAO
    {
        public List<ShoppingList> getList(int basketId)
        {
            connect();
            List<ShoppingList> shopingList = new List<ShoppingList>();
            try
            {
                Logger.log.Info("Выполнение запроса на получение списка покупок из корзины с Id = " + basketId);
                string sql = "SELECT*FROM Shopping_list where Id_Basket="+basketId;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ShoppingList shop = new ShoppingList()
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        basketId = Convert.ToInt32(reader["Id_Basket"]),
                        productId = Convert.ToInt32(reader["Id_Product"]),
                        statusId = Convert.ToInt32(reader["Status"])
                    };
                    shopingList.Add(shop);
                }
                reader.Close();
                return shopingList;
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
                Logger.log.Info("Выполнение запроса на добавление товара с Id = " + productId + " в список покупок из корзины с Id = " + basketId);
                string sql = "INSERT INTO Shopping_list (Id_Basket, Id_Product, Status) VALUES (@1, @2, @3)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", basketId);
                cmd.Parameters.AddWithValue("@2", productId);
                cmd.Parameters.AddWithValue("@3", 2);
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

        public void paid(int basketId)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на покупку товаров из списка заказов из корзины с Id = " + basketId);
                string sql = "UPDATE Shopping_list SET Status=" + 1 + " where Id_Basket=" + basketId;
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