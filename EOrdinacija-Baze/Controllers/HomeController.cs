using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Login(Users u)
        {
            if (ModelState.IsValid) 
            {
                
                using (BazaPodatakaEntities dc = new BazaPodatakaEntities()) {
                    var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        var c = dc.Doktori.Where(a => a.UserID.Equals(v.UserID)).FirstOrDefault();
                        if (c!=null)
                        {
                            ViewBag.ime = c.Ime_;
                            Session["LogedUser"] = v.Username.ToString();
                            return RedirectToAction("Index", "Doktor");
                        }
                        else  {
                            Session["LogedUser"] = v.Username.ToString();
                            return RedirectToAction("Index", "Pacijent");
                        }
                    }
                    else {
                        ViewBag.Message = "Pogrešni podaci";
                        return View("Login");
                    }
                }
            }
            return View(u);
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
