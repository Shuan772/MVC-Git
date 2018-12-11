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
            var z = Kategorien.GetAll();

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
            uint t = Convert.ToUInt32(id);
            var p = Produkte.GetByID(t);
            double price = 0.0;
            if (Session["role"]== null)
            {
                 price = Produkte.GetPrice("Gast" , p);
            }
            else
            {
                Benutzer b = new Benutzer();
                b = Benutzer.GetByNutzername(Session["name"].ToString());
                 price = Produkte.GetPrice(Benutzer.Rolle(b) , p );
            }
            ViewData["p"] = p;
            ViewData["price"] = price;
            return View();
        }
    
    }
}