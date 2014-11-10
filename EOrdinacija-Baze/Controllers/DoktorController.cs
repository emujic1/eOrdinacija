using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOrdinacija_Baze.Controllers
{
    public class DoktorController : Controller
    {
         private int id_logovanog_doktora;
        // GET: /Doktor/
        
        public ActionResult Index(string id_user)
        {
            id_logovanog_doktora = Int16.Parse(id_user);
           // var db = new BazaPodatakaEntities();
           // var ime = db.Doktori.Where(a => a.IdDoktora.Equals(id_logovanog_doktora)).FirstOrDefault();
           // ViewBag.ime = ime;
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
                            Mjesto_rodjenja = p.Mjesto_rodjenja,
                            Ime_oca=p.Ime_oca
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
        {
            var db=new BazaPodatakaEntities();
            var imena = db.Pacijenti.Select(a => a.Ime).ToList();
            var prezimena = db.Pacijenti.Select(a => a.Prezime).ToList();
            var ime_oceva = db.Pacijenti.Select(a => a.Ime_oca).ToList();

            List<SelectListItem> items = new List<SelectListItem>();
            for(int i=0; i<imena.Count; i++) {
                items.Add(new SelectListItem { Text = imena[i].ToString()+"("+ime_oceva[i]+")"+prezimena[i].ToString(), Value = i.ToString() });
            }
            ViewBag.imena = items;

           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ZakaziTermin(Termini t,int value)
        {
            return RedirectToAction("Index","Doktor");
        }
    }
}
