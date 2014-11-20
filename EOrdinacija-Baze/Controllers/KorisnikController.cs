using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOrdinacija_Baze.Controllers
{
    public class KorisnikController : Controller
    {
        //
        // GET: /Korisnik/
        
        public ActionResult Index(int id)
        {
            eOrdinacijaEntities1 dc = new eOrdinacijaEntities1();

            var tmp = dc.Role.Where(a => a.idRole.Equals(id)).FirstOrDefault();
            
            if (tmp.Privilegije.add_doktor)
            {
                return View("DodajKorisnika");
            }
            return View();
               
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUposlenik(Uposlenik u) {
            
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveKorisnik(Korisnik u)
        {
            int k=2;
            eOrdinacijaEntities1 db = new eOrdinacijaEntities1();
            db.Korisnik.Add(new Korisnik
            {
                Ime = u.Ime,
                Prezime = u.Prezime,
                Ime_oca=u.Ime_oca,
                Prebivalište = u.Prebivalište,
                Email = u.Email,
                Broj_licne = u.Broj_licne,
                Datum_rodjenja = u.Datum_rodjenja,
                Mjesto_rodjenja=u.Mjesto_rodjenja,
                idRole = k,
                JMBG = u.JMBG,
                Password = u.Password,
                Username = u.Username,
                Telefon=u.Telefon
            });
            db.SaveChanges();
            
            return View("DodajUposlenika");
        }

    }
}
