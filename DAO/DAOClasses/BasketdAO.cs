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
        public void create(Basket t)
        {
            connect();
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
            finally
            {
                disconnect();
            }
        }

        public void delete(int id)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на удаление корзины с Id = " + id);
                string sql = "DELETE FROM Basket where Id=" + id;
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

        public List<Basket> getAll()
        {
            connect();
            List<Basket> basketList = new List<Basket>();
            try
            {
                Logger.log.Info("Выполнение запроса на получение списка всех корзин");
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
            finally
            {
                disconnect();
            }
        }

        public Basket getOne(int id)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на получение корзины");
                string sql = "SELECT*FROM Basket where Id=" + id;
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
                    reader.Close();
                    return basket;
                }
                return null;
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

        public void update(int id, Basket t)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на обновление корзины с Id = " + id);
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
            finally
            {
                disconnect();
            }
        }

        public void totalUpdate(int id)
        {
            connect();
            try
            {
                ShoppingListDAO shopListDAO = new ShoppingListDAO();
                List<ShoppingList> shopList = shopListDAO.getList(id);
                int price = 0; int statusPaid = 0; int statusNotPaid = 0;
                foreach (var pr in shopList)
                {
                    Product product = new ProductDAO().getOne(pr.productId);
                    price += product.price;
                    if (pr.statusId == 1)
                        statusPaid++;
                    else
                        statusNotPaid++;
                }
                int status = 2;
                if (statusPaid > 0 && statusNotPaid > 0)
                    status = 3;
                else
                    if (statusNotPaid == 0)
                    status = 1;
                else
                    status = 2;

                string sql = "UPDATE Basket SET Status=@1, Total_price=@2 where Id=" + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", status);
                cmd.Parameters.AddWithValue("@2", price);
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
    }
}