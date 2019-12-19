using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Computer_Store.DAO.DAOClasses;
using Computer_Store.DAO.Models;


namespace Computer_Store.Controllers
{

    public class HomeController : Controller
    {
        BasketDAO basketDAO = new BasketDAO();

        public ActionResult Index()
        {
            Logger.initLogger();
            foreach(var basket in basketDAO.getAll())
            {
                basketDAO.totalUpdate(basket.id);
            }
            return View(basketDAO.getAll());
        }

        [Authorize(Roles ="Manager, Seller")]
        public ActionResult Create()
        {
            ViewData["clientList"] = new ClientDAO().getAll();
            return View();
        }

        [Authorize(Roles = "Manager, Seller")]
        public ActionResult CreateBasket(int basketId)
        {
            try
            {
                basketDAO.create(basketId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            } 
        }

        [Authorize(Roles = "Manager, Seller")]
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(basketDAO.getOne(id));
        }

        [Authorize(Roles = "Manager, Seller")]
        // POST: Product/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, Basket basket)
        {
            try
            {
                basketDAO.update(id, basket);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        [Authorize(Roles = "Manager, Seller")]
        public ActionResult Details(int id)
        {
            ViewData["basketId"] = id;
            ViewData["totalPay"] =  new ShoppingListDAO().totalPayable(id);
            return View(basketDAO.getOne(id));
        }

        [Authorize(Roles = "Manager, Seller")]
        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View(basketDAO.getOne(id));
        }

        [Authorize(Roles = "Manager")]
        // POST: Product/Delete/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                basketDAO.delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }

        // Поиск товара
        // GET:
        [Authorize(Roles = "Manager, Seller")]
        public ActionResult Search(int basketId)
        {
            ViewData["basketId"] = basketId;
            return View(new ProductDAO().getAll());
        }

        // POST:
        [Authorize(Roles = "Manager, Seller")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(SearchParameters sp, int basketId)
        {
            ViewData["basketId"] = basketId;
            return View(new ProductDAO().getAll(sp));
        }

        [Authorize(Roles = "Manager, Seller")]
        public ActionResult AddShoppingList(int productId, int basketId)
        {
            try
            {
                ShoppingListDAO shopListDAO = new ShoppingListDAO();
                shopListDAO.add(productId, basketId);
                return RedirectToAction("Search", "Home", new { basketId});
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction("Search", "Home", new { basketId });
            }
        }

        [Authorize(Roles = "Manager, Seller")]
        public ActionResult AddOrderList(int productId, int basketId)
        {
            try
            {
                OrderListDAO orderListDAO = new OrderListDAO();
                orderListDAO.add(productId, basketId);
                return RedirectToAction("Search", "Home", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction("Search", "Home", new { basketId });
            }
        }

        [Authorize(Roles = "Manager, Seller")]
        public ActionResult ToPaidShoppingList(int basketId)
        {
            try
            {
                ShoppingListDAO shopListDAO = new ShoppingListDAO();
                shopListDAO.paid(Convert.ToInt32(basketId));
                ViewData["basketId"] = basketId;
                foreach (var n in shopListDAO.getList(basketId))
                {
                    if (n.statusId == 2)
                    new ProductDAO().paid(n.productId, new ProductDAO().getOne(n.productId).amount);
                }

                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ToOrderOrderList(int basketId)
        {
            try
            {
                OrderListDAO orderListDAO = new OrderListDAO();
                orderListDAO.order(Convert.ToInt32(basketId));
                ViewData["basketId"] = basketId;
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
        }

        [Authorize(Roles = "Manager, Seller")]
        public ActionResult ToTransportFromOrderToShop(int basketId)
        {
            try
            {
                OrderListDAO orderListDAO = new OrderListDAO();
                ShoppingListDAO shopListDAO = new ShoppingListDAO();
                List<OrderList> orderList = orderListDAO.getList(basketId);
                ViewData["basketId"] = basketId;
                foreach (var product in orderList)
                {
                    if (product.statusId == 5)
                    {
                        shopListDAO.add(product.productId, basketId);
                        orderListDAO.delete(product.productId, basketId);
                    }
                }
                orderList.Clear();
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult DeliveredProduct(int productId, int basketId)
        {
            try
            {
                OrderListDAO orderListDAO = new OrderListDAO();
                orderListDAO.deliveredUpdate(productId, basketId);
                ViewData["basketId"] = basketId;
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
        }

        public ActionResult RemoveFromShoppingList(int basketId, int productId)
        {
            try
            {
                ShoppingListDAO shopListDAO = new ShoppingListDAO();
                shopListDAO.remove(productId);

                ViewData["basketId"] = basketId;
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
        }

        public ActionResult RemoveFromOrderList(int basketId, int productId)
        {
            try
            {
                OrderListDAO orderListDAO = new OrderListDAO();
                orderListDAO.remove(productId);

                ViewData["basketId"] = basketId;
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
            catch (Exception e)
            {
                Logger.log.Error(e.Message);
                return RedirectToAction(basketId.ToString(), "Home/Details", new { basketId });
            }
        }
    }
}