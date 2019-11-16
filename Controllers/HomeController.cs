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
            Logger.initLogger(); // может нам вообще не надо его вызывать ? ПОТЕСТИТЬ
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
        public ActionResult Search()
        {
            return View(new ProductDAO().getAll());
        }

        // POST:
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(String s)
        {
            return View();
        }

    }
}