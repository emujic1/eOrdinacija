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
        [ValidateAntiForgeryToken]
        public ActionResult SaveKorisnik(Korisnik u)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();

            if (u.Datum_rodjenja > DateTime.Now)
            {
                ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                return View("DodajKorisnika");

            }
            var user = db.Korisnik.Where(a => a.Username.Contains(u.Username)).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("CustomError1", "Username vec postoji");
                return View("DodajKorisnika");

            }

            db.Privilegije.Add(new Privilegije
            {
                add_doktor = false,
                add_pacijent = false,
                del_doktor = false,
                del_pacijent = false,
                modify_doktor = false,
                modify_kartona = false,
                modify_pacijent = false,
                add_new_privilegije = false,
                pregled_kartona = false,
                ažuriranje_opreme = false,
                zakazivanje_termina = false,
                imePrivilegije = u.Username
            });
            db.SaveChanges();
            Privilegije p = db.Privilegije.Where(a => a.imePrivilegije == u.Username).FirstOrDefault();
            try
            {

                db.Korisnik.Add(new Korisnik
                {
                    Ime = u.Ime,
                    Prezime = u.Prezime,
                    Ime_oca = u.Ime_oca,
                    Prebivalište = u.Prebivalište,
                    Email = u.Email,
                    Broj_licne = u.Broj_licne,
                    Datum_rodjenja = u.Datum_rodjenja,
                    Mjesto_rodjenja = u.Mjesto_rodjenja,
                    idPrivilegije = p.idPrivilegije,
                    JMBG = u.JMBG,
                    Password = u.Password,
                    Username = u.Username,
                    Telefon = u.Telefon
                });
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return View("DodajKorisnika");
            }
            ViewBag.poruka = "Korisnik uspješno spremljen";
            return View("Login");

        }
        public ActionResult Registracija()
        {

            return View("DodajKorisnika");
        }
        
        }


}
