﻿using Computer_Store.DAO.DAOClasses;
using Computer_Store.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Computer_Store.Controllers
{
    public class ClientController : Controller
    {
        ClientDAO clientDAO = new ClientDAO();
        // GET: Client
        public ActionResult Index()
        {
            return View(clientDAO.getAll());
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Client client)
        {
            try
            {
                clientDAO.create(client);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View(clientDAO.getOne(id));
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Client client)
        {
            try
            {
                clientDAO.update(id, client);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View(clientDAO.getOne(id));
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Client client)
        {
            try
            {
                clientDAO.delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
