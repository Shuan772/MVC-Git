using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBWT_Paket_5.Models;


namespace DBWT_Paket_5.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
		 [HttpPost]
        public ActionResult Check()
        {
          
            var name = Request["name"].ToString();
            var password = Request["password"].ToString();
            string teststr = "";
            teststr = Benutzer.VerifyBenutzer(password, name);
     
			 if (teststr == "False")
             {
                ViewBag.altname = name;
                ViewBag.altpw = password;
                return View("~/Views/Login/Falsch.cshtml");
             }
			 if (teststr == "True")
			 {
                        Benutzer test = new Benutzer();
                        test = Benutzer.GetByNutzername(name);
                        Benutzer.SetLogin(test);
						Session["name"] = name;
						Session["role"] = Benutzer.Rolle(test);

			 return View("~/Views/Login/True.cshtml");
            }
            else
            {
                ViewBag.message = teststr;
                ViewBag.altname = name;
                ViewBag.altpw = password;
                return View("~/Views/Login/Index.cshtml");
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            if(Session["role"] != null)
            {
                Session["role"] = null;
            }
            if (Session["name"] != null)
            {
                Session["name"] = null;
            }
            return View("~/Views/Login/Index.cshtml");
        }

        public ActionResult Registrierung()
        {
            if(Session["name"] != null)
            {
                return View("~/Views/Login/NoYouDont.cshtml");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult NeuerBenutzer()
        {
            if(Request["Nutzername"] == "")
            {
                ModelState.AddModelError("Nutzername", "Nutzername is required");
            }
            if (Benutzer.GetByNutzername(Request["Nutzername"]).Nummer != 0)
            {
                ModelState.AddModelError("Nutzername", "Nutzername wird schon verwendet.");
            }
            if (Request["E_Mail"] == "")
            {
                ModelState.AddModelError("E_Mail", "E_Mail is required");
            }
            if (Request["Vorname"] == "")
            {
                ModelState.AddModelError("Vorname", "Vorname is required");
            }
            if (Request["Nachname"] == "")
            {
                ModelState.AddModelError("Nachname", "Nachname is required");
            }
            if (Request["password"].Length < 8 || Request["passwordwdh"].Length < 8)
            {
                ModelState.AddModelError("password", "Beide Passwort Felder müssen ausgefüllt werden.");
            }
            if (Request["password"] != Request["passwordwdh"])
            {
                ModelState.AddModelError("password", "Beide Passwort Felder müssen gleich sein.");
            }
            if (Request["ISA"] == "Gast" && (Request["Grund"] == "" || Request["Ablaufdatum"] == "" ))
            {
                ModelState.AddModelError("Gast", "Als Gast muss ein Grund und ein Ablauf Datum angegeben werden.");
            }
            if (Request["ISA"] == "FH" && Request["Student"] != "1" && Request["Mitarbeiter"] != "1")
            {
                ModelState.AddModelError("Gast", "Als FH Angehöriger muss man Student oder/und Mitarbeiter sein.");
            }

            if (ModelState.IsValid)
            {
                Benutzer Neu = new Benutzer();
                Studenten Stu = new Studenten();
                Gast gast = new Gast();
                Mitarbeiter Ma = new Mitarbeiter();
                int State = 0;
                Neu.Anlegedatum = DateTime.Now;
                Neu.Aktiv = false;
                Neu.E_Mail = Request["E_Mail"];
                Neu.Nutzername = Request["Nutzername"];
                Neu.Nachname = Request["Nachname"];
                Neu.Vorname = Request["Vorname"];
                if(Request["Geburtsdatum"] != "")
                {
                    Neu.Geburtsdatum = DateTime.Parse(Request["Geburtsdatum"]);
                }
                if (Request["ISA"] == "Gast")
                {
                    Neu.ISA = "Gast";
                    gast.Ablaufdatum = DateTime.Parse(Request["Ablaufdatum"]);
                    gast.Grund = Request["Grund"];
                }
                if (Request["Student"] == "1")
                  {
                    Neu.ISA = "FHAngehörige";
                    Stu.Studiengang = Request["Studiengang"];
                    Stu.Matrikelnummer = Int32.Parse(Request["Matrikelnummer"].ToString());
                    State = 1;
                }
                if (Request["Mitarbeiter"] == "1")
                {
                    Neu.ISA = "FHAngehörige";
                    if (Request["Telefon"] != "")
                    {
                        Ma.Telefon = Request["Telefon"];
                    }
                    if (Request["Büro"] != "") { 
                        Ma.Büro = Request["Büro"];
                    }
                    State = 2;
                    if (Request["Student"] == "1")
                    {
                        State = 3;
                    }
                }
                if (Benutzer.Create(Neu, gast, Ma, Stu , State , Request["password"]))
                {
                    ModelState.AddModelError("Insert", "ERfolgreich inserted");
                    return View("~/Views/Login/Registrierung.cshtml");
                }
                else
                {
                    ModelState.AddModelError("Insert", "Fehler beim Createn");
                    return View("~/Views/Login/Registrierung.cshtml");
                }
            }
            return View("~/Views/Login/Registrierung.cshtml");
        }
    }
}