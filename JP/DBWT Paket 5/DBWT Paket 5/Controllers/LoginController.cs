using DBWT_Paket_5.Models;
using System.Web.Mvc;
namespace DBWT_Paket_5.Controllers
{
    public class LoginController : Controller
    {
      
     
        // GET: /User/
        [HttpPost]
        public ActionResult login()
        {
            Login test = new Login();
            var name = Request["username"];
            var password = Request["password"];
            bool success = false;
            success = Login.login(password, name);
            bool aktiv = false;
            aktiv = test.isactiv(name.ToString());
            if (!aktiv) { return View("~/Views/Login/NotActive.cshtml"); }
            if (success == true)
            {

                Session["name"] = name;
                Session["role"] = Login.rolle(name);
                Benutzer ben1 = new Benutzer();
                ben1.LetzterLogin = ben1.GetLetzterLogin(Session["name"].ToString());
                ben1.SetLetzterLogin(name);
                return View("~/Views/Login/SuccessView.cshtml", model: ben1);
            }
         
            return View("~/Views/Login/FailedView.cshtml");
        }


        public ActionResult IndexLogin()
        {
          
            return View();
        }

        // GET: Login
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

     

        // GET: Login
      
    }
}
