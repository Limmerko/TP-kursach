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
        public ProductDAO()
        {
            connect();
        }
        public void create(Product t)
        {
            try
            {
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
        }

        public void delete(int id)
        {
            try
            {
                string sql = "DELETE FROM Product where Id=" + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Logger.log.Error(e.Message);
            }
        }

        public List<Product> getAll()
        {
            List<Product> productList = new List<Product>();
            try
            {
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
        }

        public List<Product> getAll(SearchParameters sp)
        {
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
        }

        public Product getOne(int id)
        {
            try
            {
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
                    return product;
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

        public void update(int id, Product t)
        {
            try
            {
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
        }
    }
}