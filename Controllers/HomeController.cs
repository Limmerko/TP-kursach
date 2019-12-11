﻿using System;
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

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind (Exclude ="Id")] Basket basket)
        {
            try
            {
                basketDAO.create(basket);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            } 
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(basketDAO.getOne(id));
        }

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

        public ActionResult Details(int id)
        {
            ViewData["basketId"] = id;
            return View(basketDAO.getOne(id));
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View(basketDAO.getOne(id));
        }

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
        public ActionResult Search(int basketId)
        {
            ViewData["basketId"] = basketId;
            return View(new ProductDAO().getAll());
        }

        // POST:
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(SearchParameters sp, int basketId)
        {
            ViewData["basketId"] = basketId;
            return View(new ProductDAO().getAll(sp));
        }

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
    }
}