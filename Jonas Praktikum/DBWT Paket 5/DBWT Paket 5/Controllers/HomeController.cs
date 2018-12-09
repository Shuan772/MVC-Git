using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToDB;
using System.Web.Mvc;
using DBWT_Paket_5.Models;
using DataModels;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DBWT_Paket_5.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            bool isbackend = false;
            Login be = new Login();
            if (!string.IsNullOrEmpty(Session["name"] as string)){
                isbackend = be.isBE(Session["name"].ToString());
                
            }
            if (isbackend) { return RedirectToAction("Backend"); }
            else  return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Backend()
        {
            using (var db = new DataModels.PraktikumDB())
            {
                //LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;

                // letzter login
                var q = from z in db.FeNutzer
                        where z.Aktiv != false
                        select z;

                // letzte registrierung
                var qq = from z in db.FeNutzer
                         where z.Aktiv == false
                         select z;

                // Sortiert und ein Limit von nur 5
                q = q.OrderByDescending(z => z.LetzterLogin).Take(5);
                qq = qq.OrderByDescending(z => z.LetzterLogin).Take(5);

                ViewBag.llogin_rollen = q.ToList();
                ViewBag.lregistrierung_rollen = qq.ToList();
            }
            return View();
        }
        // GET: Home/Create
        public ActionResult aktivieren(int id = 0)
        {
            using (var db = new DataModels.PraktikumDB())
            {
                //LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
                DataModels.FeNutzer fe = db.FeNutzer.Find(Convert.ToUInt32(id));

                fe.Aktiv = true;

                db.Update(fe);
            }
            return RedirectToAction("Backend");
        }
        public ActionResult deaktivieren(int id = 0)
        {
            using (var db = new DataModels.PraktikumDB())
            {
                //LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
                DataModels.FeNutzer fe = db.FeNutzer.Find(Convert.ToUInt32(id));
                if (fe.Loginname != "wardaschka")
                {
                    fe.Aktiv = false;
                }
                db.Update(fe);
            }
            return RedirectToAction("Backend");
         

        }
        public ActionResult xmlsite()
        {
            Benutzer person = new Benutzer();
            List<XElement> l = null;
            var doc = XDocument.Load(Server.MapPath("~/App_Data/Speiseplan.xml"));
            XElement root = doc.Root;
            var q = from z in root.Descendants("Menu")
                    select z;
            var db = new DataModels.PraktikumDB();
            Datenverarbeitung dv = new Datenverarbeitung();
            foreach (var v in q.Elements("Produkte").Elements("Produkt"))
            {
               
                bool has = db.Produkts.Any(p => p.ID == Convert.ToInt32(v.Attribute("ProduktID").Value));
                if (!has)
                {
                    v.Remove();
                }
            }

            foreach (var v in q.Elements("Produkte").Elements("Produkt"))
            {
                bool has = db.Produkts.Any(p => p.ID == Convert.ToInt32(v.Attribute("ProduktID").Value));
                if (!has)
                {
                    v.Remove();
                }
            }

            foreach (var v in q.Elements("Produkte").Elements("Produkt"))
            {
                DataModels.Produkt p1 = db.Produkts.FirstOrDefault(p => p.ID == Convert.ToInt32(v.Attribute("ProduktID").Value));
                Bild p2 = db.Bilds.FirstOrDefault(p => p.Id == p1.Bildid);
                Preis p3 = db.Preis.FirstOrDefault(p => p.ID == p1.Bildid);
                XElement x = null;
                XElement xx = null;

                if (v.Parent.Parent.Attribute("Highlight") != null)
                {
                    x = new XElement("Bild", dv.blobToBase64(p2.Binaerdaten));
                    v.Add(x);
                }

                if (person.id != 0)
                {
                    if (person.rolle == "student")
                    {
                        x = new XElement("preisVon", "Student");
                        xx = new XElement("anzeigePreis", p3.Studentenbetrag.ToString("C"));
                    }
                    if (person.rolle == "mitarbeiter")
                    {
                        x = new XElement("preisVon", "Mitarbeiter");
                        xx = new XElement("anzeigePreis", p3.Mitarbeiterbetrag.ToString("C"));
                    }
                    if (person.rolle == "gast")
                    {
                        x = new XElement("preisVon", "Gast");
                        xx = new XElement("anzeigePreis", p3.Gastbetrag.ToString("C"));
                    }
                }
                else
                {
                    x = new XElement("preisVon", "Gast");
                    xx = new XElement("anzeigePreis", p3.Gastbetrag.ToString("C"));
                }
                v.Add(x);
                v.Add(xx);

                x = new XElement("beschreibung", p1.Beschreibung);
                v.Add(x);
                x = new XElement("titel", p1.Beschreibung);
                v.Add(x);

            }

            l = q.ToList();
            ViewBag.xml = l;
        
            

            return View();

         
        }


        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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
