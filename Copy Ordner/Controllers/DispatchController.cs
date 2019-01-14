using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel;
namespace DBWT_Paket_5.Controllers
{
    public struct MahlListe
    {
        public MahlItem Mahlzeit;
    }
    public struct Benutzerx
    {
        public string Vorname;
        public string Nachname;
        public string Nutzername;
        public string EMail;

    }
    public struct MahlItem
    {
        public uint Anzahl;
        public string Kategorie;
        public string Name;
        public uint Vorrat; 
    }
    public struct jsonreturn
    {
        public Benutzerx Benutzer;
        public DateTime? Abholung;
        public uint Bestellnummer;
        public List<MahlListe>  Mahlzeiten;
    }

    public class DispatchController : Controller
    {
        // GET: Dispatch
        public JsonResult Bestellungen()
        {
            var headers = Request.Headers;
            /*
             try { 
                if (headers["X-Authorize"] != "o2e1s1dpt4nwhwe")
                {
                    // hier error code return.

                    return Json( System.Net.HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            */
            

            List<jsonreturn> jret = new List<jsonreturn>();
            using (var MensaContext = new Mensa())
            {
                var query = from best in MensaContext.Bestellungen
                            join be in MensaContext.Benutzer on best.Benutzer equals be.Nummer
                            where best.Abholzeitpunkt <= DateTime.Now.AddHours(1) && best.Abholzeitpunkt >= DateTime.Now
                            //where be.Nutzername.Contains("db")
                            //select new {Vorname = be.Vorname, Nachname = be.Nachname};
                            select best ;

                var query2 = from bi in MensaContext.Bilder
                             select bi;

                try
                {
                    var list = query.ToList();
                    foreach( var item in list)
                    {
                        jsonreturn tmp = new jsonreturn();
                        tmp.Abholung = item.Abholzeitpunkt;
                        tmp.Bestellnummer = item.Nummer;

                        var queryNutzer = from be in MensaContext.Benutzer
                                          where be.Nummer == item.Benutzer
                                          select be;

                        tmp.Benutzer.EMail = queryNutzer.ToArray()[0].EMail;
                        tmp.Benutzer.Nachname = queryNutzer.ToArray()[0].Nachname;
                        tmp.Benutzer.Vorname = queryNutzer.ToArray()[0].Vorname;
                        tmp.Benutzer.Nutzername = queryNutzer.ToArray()[0].Nutzername;
                        
                        var queryBXM = from bxm in MensaContext.Mahlzeitenxbestellungen
                                       join ma in MensaContext.Mahlzeiten on bxm.Mahlzeiten equals ma.ID 
                                       join kat in MensaContext.Kategorien on ma.Kategorie equals kat.ID
                                          where bxm.Bestellungen == item.Nummer
                                          select new { bxm.Anzahl, ma.Vorrat, ma.Name, kat.Bezeichnung };
                        List<MahlListe> mahllist = new List<MahlListe>();
                        foreach (var bxm in queryBXM.ToList())
                        { 
                            
                            MahlListe tmpmahllist = new MahlListe();

                            tmpmahllist.Mahlzeit.Anzahl = bxm.Anzahl;
                            tmpmahllist.Mahlzeit.Vorrat = (uint)bxm.Vorrat;
                            tmpmahllist.Mahlzeit.Name = bxm.Name;
                            tmpmahllist.Mahlzeit.Kategorie = bxm.Bezeichnung;
                            mahllist.Add(tmpmahllist);
                        }
                        tmp.Mahlzeiten = mahllist;
                        jret.Add(tmp);
                    }
                    return Json(jret, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    var x = e.Message;
                    return Json(x, JsonRequestBehavior.AllowGet);
                }

            }

        }
    }
}