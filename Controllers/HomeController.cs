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
                return View("Index");
            }
            catch
            {
                return View("Create");
            } 
        }

        public ActionResult Details(int id)
        {
            List<Basket> basket = basketDAO.getAll();
            ViewData["basketId"] = id;
            return View(basket[id]);
        }

        






        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}