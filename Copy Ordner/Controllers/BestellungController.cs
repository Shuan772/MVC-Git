using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
using LinqToDB;

namespace DBWT_Paket_5.Controllers
{
    public class BestellungController : Controller
    {

        // GET: Bestellung
        public ActionResult Index()
        {
            try
            {
                ViewBag.message = TempData["message"].ToString();
                ViewBag.error = TempData["error"].ToString();
            }
            catch { }
                        if (Session["name"] == null)
            {
                TempData["message"]= "Loggen sie sich ein um diese Funktion zu Nutzten.";
                return Redirect("/Login/index");
            }
            //wir bruachen : Preis , MahlID, Anzahl, Name  ,
            Dictionary<int ,string[]> retDict = new Dictionary<int ,string[]>();
            //Cookie einfügen , um Preis und Name ergänzen 
            ViewData["dict"] = retDict;
            var name = Session["name"].ToString();
            if (HttpContext.Request.Cookies.Get("dbwt") != null)
            {
                // cookie auslesen
                HttpCookie exists = HttpContext.Request.Cookies.Get("dbwt");
                try
                {

                    Dictionary<string, Dictionary<int, int>>  fromCookie = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, int>>>(exists.Value);
                    if (fromCookie != null && fromCookie.ContainsKey(name))
                    {
                        // wenn alles passt
                        Dictionary<int, int> inhalt = fromCookie[name];
                        foreach (var item in inhalt)
                        {
                            var anz = item.Value;
                            var id = item.Key;
                            var price = 99.99;
                            var itemname = "default";
                            var vorrat = 1; 
                            //DB abfrage
                            DBWT_Paket_5.Models.Produkte Mahl = DBWT_Paket_5.Models.Produkte.GetByID(Convert.ToUInt32(id));
                            price = DBWT_Paket_5.Models.Produkte.GetPrice(DBWT_Paket_5.Models.Benutzer.Rolle(DBWT_Paket_5.Models.Benutzer.GetByNutzername(Session["name"].ToString())), Mahl);
                            itemname = Mahl.Name;
                            vorrat = Mahl.Vorrat;
                            string[] test = { anz.ToString(), price.ToString(), itemname , vorrat.ToString() };
                            retDict.Add(id, test);
                        }
                     

                        ViewData["dict"] = retDict;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.fromCookie = e.Message;
                }

            }
            else
            {
                ViewBag.message = "Warenkorb ist Leer";
            }
            return View();
        }

        // GET: Bestellung/Details/5
      
        // GET: Bestellung/Create
        [HttpPost]
        public ActionResult ChangeAmount()
        {
            // in dict packen.
            Dictionary<int, int> Best = new Dictionary<int, int>();
            try
            {
                int zaehler = 1; 
                while(true)
                {
                    string astr = "anzahl " + zaehler;
                    string idstr = "id " + zaehler;
                    var test = Request[astr].ToString();
                    if (Convert.ToDouble(Request[astr].ToString()) > 0)
                    { 
                        Best.Add(
                            (int)Convert.ToDouble(Request[idstr].ToString()),
                            (int)Convert.ToDouble(Request[astr].ToString())
                            );
                    }
                    zaehler++;
                }
            }
            catch
            {}
            var name = Session["name"].ToString();
            Dictionary<string, Dictionary<int, int>> fromCookie = new Dictionary<string, Dictionary<int, int>>();
            fromCookie.Add(name, Best);
            HttpCookie c = new HttpCookie("dbwt");
            c.Value = JsonConvert.SerializeObject(fromCookie);
            c.Expires = DateTime.Now.AddHours(2);
            HttpContext.Response.Cookies.Set(c);
            TempData["message"] = "Änderungen Gespeichert.";
            return RedirectToAction("index");
        }

        public ActionResult delete()
        {
            if (HttpContext.Request.Cookies.Get("dbwt") != null)
            {
                // cookie auslesen
                HttpCookie exists = HttpContext.Request.Cookies.Get("dbwt");
                try
                {
                    // cookie-Wert in ein Objekt umwandeln (geben Sie in < > an, in welchen Typ)

                    //exists.Expires = DateTime.MinValue;
                    Dictionary<string, Dictionary<int, int>> ret = new Dictionary<string, Dictionary<int, int>>();
                    ret.Add(Session["name"].ToString(), new Dictionary<int, int>());
                    exists.Value = JsonConvert.SerializeObject(ret);
                    exists.Expires = DateTime.MinValue;
                    HttpContext.Response.Cookies.Set(exists);
                    TempData["message"] = "Bestellungen gelöscht.";
                    return RedirectToAction("index");
                }
                catch (Exception e)
                {
                    TempData["error"] = "Fehler Meldung: "+e.Message;
                    return RedirectToAction("index");

                }

            }
            TempData["message"] = "Cookie war schon Weg :/.";
            return RedirectToAction("index");

        }

        [HttpPost]
        public ActionResult Bestellen()
        {
            //
            if (HttpContext.Request.Cookies.Get("dbwt") != null)
            {
                HttpCookie exists = HttpContext.Request.Cookies.Get("dbwt");
                try
                {
                    if (exists.Value != null)
                    {
                        Dictionary<string, Dictionary<int, int>> fromCookie = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, int>>>(exists.Value);
                        // cookie-Wert in ein Objekt umwandeln (geben Sie in < > an, in welchen Typ)
                        foreach (var cookie in fromCookie)
                        {
                            using (var MensaContext = new Mensa())
                            {
                                try {
                                    // Attribute für das Model p setzen
                                    // Model für den nächsten Submit mit aufnehmen

                                    // Submit löst SQL Operationen auf der DB aus

                                    //Bestellung einfügen
                                    MensaContext.BeginTransaction();
                                    Models.Benutzer User = Models.Benutzer.GetByNutzername(cookie.Key);
                                    var query = from bestmax in MensaContext.Bestellungen
                                                    //where be.Nutzername.Contains("db")
                                                    //select new {Vorname = be.Vorname, Nachname = be.Nachname};
                                                select bestmax.Nummer;

                                    Bestellungen best = new Bestellungen();
                                    best.Benutzer = (uint)User.Nummer;
                                    best.Bestellzeitpunkt = DateTime.Now;
                                    best.Abholzeitpunkt = DateTime.Parse(Request["Abholdate"].ToString());
                                    best.Endpreis = Convert.ToDouble(Request["Endpreis"].ToString());
                                    best.Nummer = query.Max() + 1;
                                    MensaContext.Insert<Bestellungen>(best);
                                    //ID bekommen und einfügen 
                                    foreach (var mahl in cookie.Value)
                                    {
                                        Mahlzeitenxbestellungen mxb = new Mahlzeitenxbestellungen();
                                        mxb.Anzahl = Convert.ToUInt32(mahl.Value);
                                        mxb.Mahlzeiten = (uint?)mahl.Key;
                                        mxb.Bestellungen = best.Nummer;
                                        MensaContext.Insert<Mahlzeitenxbestellungen>(mxb);
                                        //mxb einfügen

                                    }
                                    MensaContext.CommitTransaction();
                                }
                                catch
                                {
                                    MensaContext.RollbackTransaction();
                                }
                            }
                            
                        }
                        //Warenkorb leeren

                        Dictionary<string, Dictionary<int, int>> ret = new Dictionary<string, Dictionary<int, int>>();
                        ret.Add(Session["name"].ToString(), new Dictionary<int, int>());
                        exists.Value = JsonConvert.SerializeObject(ret);
                        HttpContext.Response.Cookies.Set(exists);
                        // zurück mit Meldung.
                        TempData["message"] = "Bestellung abgeschickt.";
                        return RedirectToAction("index");
                    }
                    else
                    {
                        TempData["message"] = "Bestellungen leer.";
                        return RedirectToAction("index");
                    }
                    

                }
                catch (Exception e)
                {
                    TempData["error"] = "Fehler Meldung: " + e.Message;
                    return RedirectToAction("index");

                }
            }
            TempData["message"] = "Bestellung abgeschickt.";
            return RedirectToAction("index");
        }
    }
}
