using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;
using System.Data.SqlClient;

namespace Computer_Store.DAO.DAOClasses
{
    public class BasketDAO : DAO, IDAO<Basket>
    {
        public BasketDAO()
        {
            connect();
        }
        public void create(Basket t)
        {
            try
            {
                Logger.log.Info("Выполнение запроса на создание новой корзины");
                string sql = "INSERT INTO Basket (Id_Client, DataOfCreation, Status, Total_price) VALUES (@1, @2, @3, @4)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", t.clientId);
                cmd.Parameters.AddWithValue("@2", DateTime.Now);
                cmd.Parameters.AddWithValue("@3", 2);
                cmd.Parameters.AddWithValue("@4", 0);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
        }

        public void delete(int id)
        {
            try
            {
                string sql = "DELETE FROM Basket where Id=" + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
        }

        public List<Basket> getAll()
        {
            List<Basket> basketList = new List<Basket>();
            try
            {
                string sql = "SELECT*FROM Basket";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Basket basket = new Basket
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        clientId = Convert.ToInt32(reader["Id_Client"]),
                        dateOfCreation = Convert.ToDateTime(reader["DataOfCreation"]),
                        statusId = Convert.ToInt32(reader["Status"]),
                        totalPrice = Convert.ToInt32(reader["Total_price"])
                    };
                    basketList.Add(basket);
                }
                reader.Close();
                return basketList;
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
                return null;
            }
        }

        public Basket getOne(int id)
        {
            try
            {
                string sql = "SELECT*FROM Basket where Id="+id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Basket basket = new Basket
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        clientId = Convert.ToInt32(reader["Id_Client"]),
                        dateOfCreation = Convert.ToDateTime(reader["DataOfCreation"]),
                        statusId = Convert.ToInt32(reader["Status"]),
                        totalPrice = Convert.ToInt32(reader["Total_price"])
                    };
                    return basket;
                }
                reader.Close();
                return null;
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
                return null;
            }
        }

        public void update(int id, Basket t)
        {
            try
            {
                string sql = "UPDATE Basket SET Id_Client=@1, DataOfCreation=@2, Status=@3, Total_price=@4 where Id=" + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", t.clientId);
                cmd.Parameters.AddWithValue("@2", t.dateOfCreation);
                cmd.Parameters.AddWithValue("@3", t.statusId);
                cmd.Parameters.AddWithValue("@4", t.totalPrice);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
        }
    }
}