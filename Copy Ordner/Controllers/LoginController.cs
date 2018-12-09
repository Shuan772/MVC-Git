using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBWT_Paket_5.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        // POST /Login/Index mit 
        //return Inaktiv 
        //return False 
        //return True
        //bei True session setzten

        //POST Login/Logout
        //return Index 


        // GET /Login/Registrieren
        // return Create View

        //POST /Login/Registrieren
        //back wenn eingaben falsch 
        // wenn I.O. dann weiter zu Create 
        //return Index mit erfolgreich registriert Acc muss noch aktiviert werden
    }
}