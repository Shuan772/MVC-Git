using DBWT_Paket_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DBWT_Paket_5.Controllers
{
    public class ZutatenController : Controller
    {
        // GET: Zutaten
        public ActionResult Index()
        {
            
            List<Zutaten> ZutatenListe = new List<Zutaten>();
            Zutaten Zutaten = new Zutaten();
            ZutatenListe = Zutaten.GetAll();
            //ViewBag.Message = ;
            
            return View(ZutatenListe);

        }

    }
}
