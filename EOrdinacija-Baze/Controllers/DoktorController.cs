using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOrdinacija_Baze.Controllers
{
    public class DoktorController : Controller
    {
        //
        // GET: /Doktor/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegistracijaPacijenta()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistracijaPacijenta(Pacijenti p)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var db = new BazaPodatakaEntities();
                    var c = db.Users.Where(a => a.Username.Equals(p.Users.Username)).FirstOrDefault();
                    if (c == null)
                    {
                        db.Users.Add(new Users { Username = p.Users.Username, Password = p.Users.Password });
                        db.SaveChanges();
                        var v = db.Users.Where(a => a.Username.Equals(p.Users.Username)).FirstOrDefault();
                        db.Pacijenti.Add(new Pacijenti
                        {
                            UserID = v.UserID,
                            Ime = p.Ime,
                            Prezime = p.Prezime,
                            Datum_rođenja = p.Datum_rođenja,
                            Mjesto_prebivališta = p.Mjesto_prebivališta,
                            Licna_karta = p.Licna_karta,
                            Mjesto_rodjenja = p.Mjesto_rodjenja
                        });
                        db.SaveChanges();
                        ViewBag.dodan = "Pacijent uspješno dodan!";
                    }
                    else
                    {
                        ViewBag.postoji = "Username vec postoji, izaberite neko drugo!";
                        return View();
                    }
                }
                catch (System.InvalidOperationException)
                {
                    ViewBag.dodan = "Greška";
                    return View();
                }
            }
            return View();
        }
        public ActionResult PregledRasporeda()
        {
            return View();
        }
        public ActionResult IzvršiPregled() 
        {
            return View();
        }
        public ActionResult ZakaziTermin()
        {/*
            var db=( from Pacijenti).ToList(); 
             List<string> imena = new List<string>();
            foreach(var k in db){
            imena.Add(k.Ime);
            }
            ViewBag.pacijenti = imena;*/
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ZakaziTermin(Termini t)
        {
            return RedirectToAction("Index","Doktor");
        }
    }
}
