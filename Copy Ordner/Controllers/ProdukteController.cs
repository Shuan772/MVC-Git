using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using DBWT_Paket_5.Models;
using Newtonsoft.Json;

namespace DBWT_Paket_5.Controllers
{
    public class ProdukteController : Controller
    {
        // GET: Produkte
        public ActionResult Index(string bez , string vege , string vega , string vorrat )
        {
            string tname = "0";
            if(bez != null)
            {
                tname = bez;
            }
            bool tvege = false;
            if (vege == "1")
            {
                tvege = true;
            }

            bool tvega = false;
            if (vega == "1")
            {
                tvega = true;
            }

            bool tvorrat = false;
            if (vorrat == "1")
            {
                tvorrat = true;
            }

            var p = Produkte.Filter(tname, tvege, tvega, tvorrat);
            var z = Models.Kategorien.GetAll();

            ViewData["Kategorien"] = z;
            ViewData["Produkte"] = p;
            ViewData["bez"] = tname;
            ViewData["vege"] = tvege;
            ViewData["vega"] = tvega;
            ViewData["vorrat"] = tvorrat;
            return View();
        }
        public ActionResult Detail(string id)
        {
            try
            {
                ViewBag.message = TempData["message"].ToString();
                ViewBag.error = TempData["error"].ToString();
            }
            catch { }
            var p = new Produkte();
            double price = 0.0;
            int vorrat = 1;
            uint xid = Convert.ToUInt32(id);
            using (var MensaContext = new Mensa())
            {
                var query = from be in MensaContext.Mahlzeiten
                            where be.ID == Convert.ToUInt32(id)
                            select be.Vorrat;
                try
                {
                    vorrat = query.Sum();
                }
                catch (Exception e)
                {
                    var x = e.Message;
                }
            }
            try { 
            uint t = Convert.ToUInt32(id);
             p = Produkte.GetByID(t);
          
            if (Session["role"]== null)
            {
                 price = Produkte.GetPrice("Gast" , p);
            }
            else
            {
                Models.Benutzer b = new Models.Benutzer();
                b = Models.Benutzer.GetByNutzername(Session["name"].ToString());
                 price = Produkte.GetPrice(Models.Benutzer.Rolle(b) , p );
            }
            ViewData["p"] = p;
            ViewData["price"] = price;
            return View();
            }catch(Exception ex)
            {
                ViewData["price"] = price;
                ViewData["p"] = p;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddToList()
        {
            int anz = 0;
            var id = Convert.ToInt32(Request["id"].ToString());
            var name = Session["name"].ToString();
            using (var MensaContext = new Mensa())
            {
                var query = from ma in MensaContext.Mahlzeiten
                            where ma.ID == id
                            select ma.Vorrat;
                try
                {
                    anz = query.ToArray()[0];
                }
                catch (Exception e)
                {
                    var x = e.Message;
                    return Json(x, JsonRequestBehavior.AllowGet);
                }

            }

            Dictionary<int, int> prod = new Dictionary<int, int>() { { Convert.ToInt32(Request["id"].ToString()), 1 }, };
            HttpCookie c = new HttpCookie("dbwt");
            Dictionary<string, Dictionary<int, int>> fromCookie = new Dictionary<string, Dictionary<int, int>>();
            fromCookie.Add(name, prod);
            if (anz < 1)
            {
                ViewBag.message = "Ausverkauft.";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            if (HttpContext.Request.Cookies.Get("dbwt") != null)
            {
                // cookie auslesen
                HttpCookie exists = HttpContext.Request.Cookies.Get("dbwt");
                try
                {
                    fromCookie = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, int>>>(exists.Value);
                    if (fromCookie != null && fromCookie.ContainsKey(name))
                    {
                        //+1
                        if (fromCookie[name].ContainsKey(id))
                        {
                            if (anz - fromCookie[name][id] < 1)
                            {
                                TempData["message"] = "ALLE Verfügbare Mahlzeit(en) bereits in Bestellung.";
                                return Redirect(Request.Headers["Referer"].ToString());


                            }
                            else
                            {
                                fromCookie[name][id]++;
                                TempData["message"] = "Mahlzeit erneut zur bestellung hinzugefügt.";
                            }
                        }
                        else
                        {
                            //neu eingabe.
                            fromCookie[name].Add(Convert.ToInt32(Request["id"].ToString()), 1);
                            TempData["message"] = "Mahlzeit hinzugefügt.";


                        }
                    }
                    else
                    {
                        //Cookie von Wem anden
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = e.Message;
                }

                // cookie neu setzten.


            }
          
            c.Value = JsonConvert.SerializeObject(fromCookie);
            c.Expires = DateTime.Now.AddHours(2);
            HttpContext.Response.Cookies.Set(c);
            return Redirect(Request.Headers["Referer"].ToString());

        }

    }


}