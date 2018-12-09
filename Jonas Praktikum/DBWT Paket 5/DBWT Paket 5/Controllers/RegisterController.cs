using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBWT_Paket_5.Models;

namespace DBWT_Paket_5
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Session["user"] as string))
            {
                return View("~/View/Register/Fail.cshtml"); //Wenn die Registrieungsseite irgendwie geöffnet wird, wenn jemand angemeldet ist: Fehlerseite.
            }
            else
            {
                Register reg = new Register();
                return View(reg);
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (!string.IsNullOrEmpty(Session["user"] as string))
            {
                return View("~/View/Register/Fail.cshtml"); //Wenn die Registrieungsseite irgendwie geöffnet wird, wenn jemand angemeldet ist: Fehlerseite.
            }
            else
            {
                Register reg = new Register();

                if (!string.IsNullOrEmpty(collection["rolle"])) //Ist eine Rolle gewählt worden?
                {
                    reg.rolle_pruefen_setzen(collection["rolle"], reg); // Ja, dann speichere die Rolle
                }
                else
                {
                    return View(reg); // nein, dann zeig Schritt 1 der Registrierung nochmal an.
                }

                reg.vorname = collection["vorname"];
                reg.nachname = collection["nachname"];
                reg.matrikelnummer = Convert.ToInt32(collection["matrikel"]);
                reg.studiengang = collection["studiengang"];
                reg.telefonnummer = collection["telefon"];
                reg.buero = collection["buero"];
                reg.grund = collection["grund"];

                reg.benutzername = collection["loginname"]; //überprüfen
                reg.email = collection["email"]; //überprüfen

                if (!reg.eingabe_korret(collection["passwort1"], collection["passwort2"], collection["loginname"], collection["email"], reg))
                {
                    return View(reg); // etwas war noch nicht korrekt, erfolgreich darf noch nicht true sein. die View wird nochmal mit alert aufgerufen.
                }

                if (collection["schritt2"] == "true")
                {
                    reg.erfolgreich = true;
                    reg.Nutzer_Anlegen();
                }
                return View(reg);
            }
        }
    }
}