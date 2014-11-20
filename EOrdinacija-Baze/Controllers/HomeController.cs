using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EOrdinacija_Baze.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Korisnik u)
        {
            
                
                using (eOrdinacijaEntities1 dc = new eOrdinacijaEntities1()) {
                    var v = dc.Korisnik.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        
                        return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Korisnik", action = "Index", id = v.idRole }));
                       
                    }
                    else {
                        ViewBag.Message = "Pogrešni podaci";
                        return View("Login");
                    }
                }
            
        }
        public ActionResult AfterLogin()
        {
            if (Session["LogedUser"] != null)
            {
                return View();
            }
            else {
                ViewBag.test = "bravisimo";
                return RedirectToAction("Index");
            }

        }
        
        }


}
