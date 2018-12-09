using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBWT_Paket_5.Models;
namespace DBWT_Paket_5.Controllers
{
    public class ProdukteController : Controller
    {
        // GET: Produkte
        public ActionResult IndexProdukte(string id)
        {
          
            
            
            int t = 0;
            if (id != "") {
                t = Convert.ToInt32(id);
            }
       //     var kats = Produkt.kats();
            var p = Produkt.produktnachkat(t);
            kategorien k = new kategorien();
            var z = k.getkats();
            ViewData["kats"] = z;
        
            ViewData["MyP"] = p;
            return View();
        }
        public ActionResult ProduktenachKats(string id)
        {
            int t = Convert.ToInt32(id);
            var p = Produkt.produktnachkat(t);
            ViewData["MyP"] = p;
            return View();
        }
        // GET: Produkte/Details/5
        public ActionResult Details(string id)
        {
            int t = Convert.ToInt32(id);
            var p = Produkt.detail(t);
            ViewData["MyP"] = p;
            return View();
        }

        // GET: Produkte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produkte/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produkte/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produkte/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produkte/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produkte/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
