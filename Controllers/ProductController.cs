using Computer_Store.DAO.DAOClasses;
using Computer_Store.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Computer_Store.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO productDAO = new ProductDAO();

        // GET: Product
        public ActionResult Index()
        {
            return View(productDAO.getAll());
        }


        // GET: Product/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [Authorize(Roles = "Manager")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Product product)
        {
            try
            {
                productDAO.create(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Product/Edit
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            return View(productDAO.getOne(id));
        }

        // POST: Product/Edit
        [Authorize(Roles = "Manager")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                productDAO.update(id, product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        [Authorize(Roles = "Manager")]
        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View(productDAO.getOne(id));
        }

        // POST: Product/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                productDAO.delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
