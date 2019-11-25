using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;

namespace Computer_Store.DAO.DAOClasses
{
    public class ClientDAO : DAO, IDAO<Client> 
    {

        public void create(Client t) 
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на добавление нового клиента");
                string sql = "INSERT INTO Client (Name, Patronymic, Surname, Phone) VALUES (@1, @2, @3, @4)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", t.name);
                cmd.Parameters.AddWithValue("@2", t.patronymic);
                cmd.Parameters.AddWithValue("@3", t.surname);
                cmd.Parameters.AddWithValue("@4", t.phone);
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
                Logger.log.Info("Выполнение запроса на удаление клиента с Id = " + id);
                string sql = "DELETE FROM Client where Id="+id;
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

        public List<Client> getAll()
        {
            connect();
            List<Client> clientList = new List<Client>();
            try
            {
                Logger.log.Info("Выполнение запроса на получение списка всех клиентов");
                string sql = "SELECT*FROM Client";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Client client = new Client
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        name = Convert.ToString(reader["Name"]),
                        patronymic = Convert.ToString(reader["Patronymic"]),
                        surname = Convert.ToString(reader["Surname"]),
                        phone = Convert.ToString(reader["Phone"])
                    };
                    clientList.Add(client);
                }
                reader.Close();
                return clientList;
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

        public Client getOne(int id)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на получение клиента с Id = " + id);
                string sql = "SELECT*FROM Client where Id="+id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                Client client = new Client();
                if (reader.Read())
                {
                    client.id = Convert.ToInt32(reader["Id"]);
                    client.name = Convert.ToString(reader["Name"]);
                    client.patronymic = Convert.ToString(reader["Patronymic"]);
                    client.surname = Convert.ToString(reader["Surname"]);
                    client.phone = Convert.ToString(reader["Phone"]);
                }
                reader.Close();
                return client;
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

        public void update(int id, Client t)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на обновление клиента с Id = " + id);
                string sql = "UPDATE Client SET Name=@1, Patronymic=@2, Surname=@3, Phone=@4 where Id=" + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", t.name);
                cmd.Parameters.AddWithValue("@2", t.patronymic);
                cmd.Parameters.AddWithValue("@3", t.surname);
                cmd.Parameters.AddWithValue("@4", t.phone);
                cmd.ExecuteNonQuery();
            }
            catch(SqlException e)
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