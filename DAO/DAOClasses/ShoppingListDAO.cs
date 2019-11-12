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
        public ShoppingListDAO()
        {
            connect();
        }
        public List<ShoppingList> getList(int basketId)
        {
            List<ShoppingList> shopingList = new List<ShoppingList>();
            try
            {
                string sql = "SELECT*FROM Shopping_list where Id_Basket="+basketId;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ShoppingList shop = new ShoppingList()
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        basketId = Convert.ToInt32(reader["Id_Basket"]),
                        productId = Convert.ToInt32(reader["Id_Product"])
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
        }
    }
}