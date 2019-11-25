using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;
using System.Data.SqlClient;

namespace Computer_Store.DAO.DAOClasses
{
    public class ProductDAO : DAO, IDAO<Product>
    {
        public void create(Product t)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на добавление нового товара");
                string sql = "INSERT INTO Product (Title, Number, Category, Producer, Price, Amount) VALUES (@1, @2, @3, @4, @5, @6)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", t.title);
                cmd.Parameters.AddWithValue("@2", t.number);
                cmd.Parameters.AddWithValue("@3", t.categoryId);
                cmd.Parameters.AddWithValue("@4", t.producer);
                cmd.Parameters.AddWithValue("@5", t.price);
                cmd.Parameters.AddWithValue("@6", t.amount);
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
                Logger.log.Info("Выполнение запроса на удаление товара с Id = " + id);
                string sql = "DELETE FROM Product where Id=" + id;
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

        public List<Product> getAll()
        {
            connect();
            List<Product> productList = new List<Product>();
            try
            {
                Logger.log.Info("Выполнение запроса на получение списка всех товаров");
                string sql = "SELECT*FROM Product";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        title = Convert.ToString(reader["Title"]),
                        number = Convert.ToString(reader["Number"]),
                        categoryId = Convert.ToInt32(reader["Category"]),
                        producer = Convert.ToString(reader["Producer"]),
                        price = Convert.ToInt32(reader["Price"]),
                        amount = Convert.ToInt32(reader["Amount"])

                    };
                    productList.Add(product);
                }
                reader.Close();
                return productList;
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

        public List<Product> getAll(SearchParameters sp)
        {
            connect();
            List<Product> productList = new List<Product>();
            int cat = 0; 
            foreach (var i in Enum.GetValues(typeof(category)))
            {
                if (i.ToString() == sp.categorySearch)
                {
                    cat = (int)i;
                }
            } 

            try
            {
                Logger.log.Info("Выполнение запроса на получение списка всех товаров с параметрами");
                string sql = "SELECT*FROM Product";
                if (sp.produserSearch != null && sp.produserSearch != "" && cat != 0)
                {
                    sql = "SELECT*FROM Product where Producer=" +"'"+sp.produserSearch+"'"+ " and Category=" + cat;
                }
                else
                {
                    if (sp.produserSearch != null && sp.produserSearch != "")
                    {
                        sql = "SELECT*FROM Product where Producer=" + "'" + sp.produserSearch + "'";
                    }
                    else
                    {
                        if (cat != 0)
                        {
                            sql = "SELECT*FROM Product where Category=" + cat;
                        }
                        else
                        {
                            sql = "SELECT*FROM Product";
                        }
                    }
                }
                 
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        title = Convert.ToString(reader["Title"]),
                        number = Convert.ToString(reader["Number"]),
                        categoryId = Convert.ToInt32(reader["Category"]),
                        producer = Convert.ToString(reader["Producer"]),
                        price = Convert.ToInt32(reader["Price"]),
                        amount = Convert.ToInt32(reader["Amount"])

                    };
                    productList.Add(product);
                }
                reader.Close();
                return productList;
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

        public Product getOne(int id)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на получение товара с Id = " + id);
                string sql = "SELECT*FROM Product where Id="+id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Product product = new Product
                    {
                        id = Convert.ToInt32(reader["Id"]),
                        title = Convert.ToString(reader["Title"]),
                        number = Convert.ToString(reader["Number"]),
                        categoryId = Convert.ToInt32(reader["Category"]),
                        producer = Convert.ToString(reader["Producer"]),
                        price = Convert.ToInt32(reader["Price"]),
                        amount = Convert.ToInt32(reader["Amount"])
                    };
                    reader.Close();
                    return product;
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

        public void update(int id, Product t)
        {
            connect();
            try
            {
                Logger.log.Info("Выполнение запроса на обновление товара с Id = " + id);
                string sql = "UPDATE Product SET Title=@1, Number=@2, Category=@3, Producer=@4, Price=@5, Amount=@6 where Id=" + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@1", t.title);
                cmd.Parameters.AddWithValue("@2", t.number);
                cmd.Parameters.AddWithValue("@3", t.categoryId);
                cmd.Parameters.AddWithValue("@4", t.producer);
                cmd.Parameters.AddWithValue("@5", t.price);
                cmd.Parameters.AddWithValue("@6", t.amount);
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