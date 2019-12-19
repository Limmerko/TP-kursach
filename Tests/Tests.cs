using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;
using System.Data.SqlClient;

namespace Computer_Store.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestSearch()
        {
            List<Product> productList = new List<Product>();
            string sql = "SELECT*FROM Product where Producer=AMD and Category=Процессор";
            SqlCommand cmd = new SqlCommand(sql, new DAO.DAO().connection);
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

            SearchParameters param = new SearchParameters();
            param.categorySearch = "Процессор";
            param.produserSearch = "AMD";

            Assert.AreEqual(productList, new DAO.DAOClasses.ProductDAO().getAll(param));
        }
    }

    //можно сделать тест, который будет проверять возвращение ошибки
}