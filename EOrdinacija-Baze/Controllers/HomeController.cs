using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Session["LogiraniKorisnik"] = null;
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
            Session["LogiraniKorisnik"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Korisnik u)
        {
            
                
                using (eOrdinacijaEntities dc = new eOrdinacijaEntities()) {
                    var v = dc.Korisnik.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogiraniKorisnik"] = v.IdKorisnika; 
                        return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Korisnik", action = "Index", id = v.IdKorisnika }));
                       
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
        [HttpPost]
        public JsonResult PostojiUsername(string UserName)
        {

            eOrdinacijaEntities db = new eOrdinacijaEntities();
            var user = db.Korisnik.Where(a=>a.Username.Contains(UserName)).FirstOrDefault();

            return Json(user == null);
        }
        [HttpPost]
        public JsonResult ProvjeraDatuma(DateTime Datum_rodjenja ) {

            return Json(Datum_rodjenja < DateTime.Now);
        }
        
        }


}
