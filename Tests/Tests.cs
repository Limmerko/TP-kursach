using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Computer_Store.DAO.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Computer_Store.Tests
{
    [TestFixture]
    public class Tests
    {
        // Проверяет работу поиска товаров
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

        //Проверяет работу поиска товаров
        [Test]
        public void TestSearchNull()
        {
            SearchParameters param = new SearchParameters();
            param.categorySearch = "0";
            param.produserSearch = "0";

            Assert.Null(new DAO.DAOClasses.ProductDAO().getAll(param));
        }

        //Проверяет, что у всех корзин стоит правильный статус
        [Test]
        public void statusPaidBasket()
        {
            bool ok = true;
            DAO.DAOClasses.BasketDAO dao = new DAO.DAOClasses.BasketDAO();
            
            foreach(var n in dao.getAll())
            {
                switch (n.statusId)
                {
                    case 1:
                        DAO.DAOClasses.ShoppingListDAO shopDaoPaid = new DAO.DAOClasses.ShoppingListDAO();
                        foreach (var p in shopDaoPaid.getList(n.id))
                            if (p.statusId != 1) { ok = false; break; }
                        break;
                    case 2:
                        DAO.DAOClasses.ShoppingListDAO shopDaoNotPaid = new DAO.DAOClasses.ShoppingListDAO();
                        foreach (var p in shopDaoNotPaid.getList(n.id))
                            if (p.statusId != 2) { ok = false; break; }
                        break;
                    case 3:
                        int paid = 0; int notpaid = 0;
                        DAO.DAOClasses.ShoppingListDAO shopDaoParPaid = new DAO.DAOClasses.ShoppingListDAO();
                        foreach (var p in shopDaoParPaid.getList(n.id))
                            if (p.statusId == 1)
                                paid++;
                            else
                                notpaid++;
                        if (paid > 0 && notpaid > 0 && n.statusId != 3)
                            ok = false;
                        break;
                }
            }

            Assert.IsTrue(ok);
        }

        //Проверяет, что у всех корзин стоит правильная стоимость
        [Test]
        public void totalPriceBasket()
        {
            bool ok = true;
            DAO.DAOClasses.BasketDAO dao = new DAO.DAOClasses.BasketDAO();

            foreach (var n in dao.getAll())
            {
                int totalPrice = 0;
                DAO.DAOClasses.ShoppingListDAO shopDao = new DAO.DAOClasses.ShoppingListDAO();
                foreach (var p in shopDao.getList(n.id))
                {
                    totalPrice += new DAO.DAOClasses.ProductDAO().getOne(p.productId).price;
                }
                if (totalPrice != n.totalPrice) { ok = false; break; }
            }

            Assert.IsTrue(ok);
        }

        [Test]
        public void Index()
        {
            Controllers.HomeController controller = new Controllers.HomeController();
            ViewResult view = controller.Index() as ViewResult;

            Assert.IsNotNull(view);
        }
    }
}

