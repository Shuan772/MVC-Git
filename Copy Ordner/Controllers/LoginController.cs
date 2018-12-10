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
                      //  Benutzer.SetLogin(test);
						Session["name"] = name;
						Session["role"] = test.Rolle;

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
            if(!String.IsNullOrEmpty(Session["role"].ToString()))
            {
                Session["role"] = null;
            }
            if (!String.IsNullOrEmpty(Session["name"].ToString()))
            {
                Session["name"] = null;
            }
            return View("~/Views/Login/Index.cshtml");
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