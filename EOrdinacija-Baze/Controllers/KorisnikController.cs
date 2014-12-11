using DHTMLX.Scheduler;
using EOrdinacija_Baze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOrdinacija_Baze.Controllers
{
    public class KorisnikController : Controller
    {
        static int idKorisnika;
         
             

        [HttpPost]
        public JsonResult PostojiUsername(string UserName)
        {

            eOrdinacijaEntities db = new eOrdinacijaEntities();
            var user = db.Korisnik.Where(a => a.Username.Contains(UserName)).FirstOrDefault();

            return Json(user == null);
        }
        [HttpPost]
        public JsonResult ProvjeraDatuma(DateTime Datum_rodjenja)
        {

            return Json(Datum_rodjenja < DateTime.Now);
        }
        
        public ActionResult HomePage() {
           
            bool add_doktor = false,
            add_pacijent = false,
            del_doktor = false,
            izm_priv = false,
            addTermin = false,
            doktor = false;

            eOrdinacijaEntities dc = new eOrdinacijaEntities();

            Korisnik k = dc.Korisnik.Find(idKorisnika);
            
            var tmp = dc.Role.Where(a => a.idRole.Equals(k.idRole)).FirstOrDefault();

            if (tmp.Privilegije.add_doktor)
            {
                add_doktor = true;
            }
            if (tmp.Privilegije.add_pacijent)
            {
                add_pacijent = true;
            }
            if (tmp.Privilegije.del_doktor)
            {
                del_doktor = true;
            }
            if (tmp.Privilegije.add_new_privilegije)
            {
                izm_priv = true;
            }
            if (tmp.Privilegije.zakazivanje_termina)
            {
                addTermin = true;
            }
            if (k.Uposlenik.Count != 0)
            {

                doktor = true;
            }
            
                ViewBag.add_doktor = add_doktor;
                ViewBag.add_pacijent = add_pacijent;
                ViewBag.del_doktor = del_doktor;
                ViewBag.izm_priv = izm_priv;
                ViewBag.addTermin = addTermin;
                ViewBag.doktor = doktor;

                var scheduler = new DHXScheduler(this);

                

                scheduler.Skin = DHXScheduler.Skins.Terrace;

                scheduler.Config.multi_day = true;

                scheduler.LoadData = true;
                scheduler.EnableDataprocessor = true;

                return View(scheduler);
                return View("HomePage",scheduler);
        }

        public ActionResult Index(int id)
        {

          
              

                idKorisnika = id;
               
                return RedirectToAction("HomePage");
           
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
            try
            {
                int k = 1;
                eOrdinacijaEntities db = new eOrdinacijaEntities();
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
                    idRole = k,
                    JMBG = u.JMBG,
                    Password = u.Password,
                    Username = u.Username,
                    Telefon = u.Telefon
                });
                db.SaveChanges();
            }
            catch (Exception e) {
                ViewBag.greska = "Username vec postoji";
                return View("DodajKorisnika");
            }

                return RedirectToAction("Login","Home"); 
           
        }
        public ActionResult Registracija() {

            return View("DodajKorisnika");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");

        }

        public ActionResult DodajUposlenika() {
                         
            eOrdinacijaEntities db = new eOrdinacijaEntities();


            
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
            
            return View();
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult DodajUposlenika(KorisnikUposlenikModel kum)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            try
            {
                int k = Convert.ToInt32(kum.Odjel.IdOdjela);

                db.Korisnik.Add(new Korisnik
                {
                    Ime = kum.Korisnik.Ime,
                    Prezime = kum.Korisnik.Prezime,
                    Ime_oca = kum.Korisnik.Ime_oca,
                    Prebivalište = kum.Korisnik.Prebivalište,
                    Email = kum.Korisnik.Email,
                    Broj_licne = kum.Korisnik.Broj_licne,
                    Datum_rodjenja = kum.Korisnik.Datum_rodjenja,
                    Mjesto_rodjenja = kum.Korisnik.Mjesto_rodjenja,
                    idRole = k,
                    JMBG = kum.Korisnik.JMBG,
                    Password = kum.Korisnik.Password,
                    Username = kum.Korisnik.Username,
                    Telefon = kum.Korisnik.Telefon
                });

                db.SaveChanges();

                var c = db.Korisnik.Where(a => a.Username.Equals(kum.Korisnik.Username)).FirstOrDefault();
                int idKor = c.IdKorisnika;


                db.Uposlenik.Add(new Uposlenik
                    {
                        Biografija = kum.Uposlenik.Biografija,
                        Zanimanje = kum.Uposlenik.Zanimanje,
                        Titula = kum.Uposlenik.Titula,
                        idKorisnika = idKor,
                        idOdjela = k
                    }

                    );

                db.SaveChanges();


            }
            catch (Exception e)
            {
                ViewBag.greska = "Doslo je do greske, pokusajte ponovo, odaberite drugi uzername";
                return View("DodajUposlenika");

            }
            return RedirectToAction("HomePage");
        }
        public ActionResult BrišiUposlenika(string ime) { 
        
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            var korisnici = from m in db.Korisnik.Where(a=>a.Uposlenik.Count>0)
                             select m;
           

            if (!String.IsNullOrEmpty(ime)) {

                korisnici = db.Korisnik.Where(a => a.Ime.Contains(ime) && a.Uposlenik.Count>0 );

            }
            return View(korisnici);
        
        }
        public ActionResult ObrišiKorisnika(String id) {

            int k = Convert.ToInt32(id);
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Korisnik o = db.Korisnik.Find(k);
            db.Korisnik.Remove(o);
            db.SaveChanges();
            return RedirectToAction("BrišiUposlenika");
        }
        public ActionResult UpravljanjePrivilegijama(string ime) {

            eOrdinacijaEntities db = new eOrdinacijaEntities();
            var korisnici = from m in db.Korisnik
                            select m;


            if (!String.IsNullOrEmpty(ime))
            {

                korisnici = db.Korisnik.Where(a => a.Ime.Contains(ime));

            }
            return View(korisnici);
        }
        public ActionResult IzmjenaPrivilegije(string id) {
            int idKorisnika = Convert.ToInt32(id);
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Korisnik k = db.Korisnik.Find(idKorisnika);
            return View(k.Role.Privilegije);
        }
        public ActionResult IzmjeniPrivilegiju(Privilegije k) {

            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Privilegije p = db.Privilegije.Find(k.idPrivilegije);

            p.modify_doktor = k.modify_doktor;
            p.modify_kartona = k.modify_kartona;
            p.pregled_kartona = k.pregled_kartona;
            p.modify_pacijent = k.modify_pacijent;
            p.zakazivanje_termina = k.zakazivanje_termina;
            p.del_pacijent = k.del_pacijent;
            p.del_doktor = k.del_doktor;
            p.ažuriranje_opreme = k.ažuriranje_opreme;
            p.add_pacijent = k.add_pacijent;
            p.add_doktor = k.add_doktor;
            p.add_new_privilegije = k.add_new_privilegije;
            db.SaveChanges();

            


            return RedirectToAction("UpravljanjePrivilegijama");
        }
        public ActionResult dodajUposlenikaVecNapravljenAcount(string ime) 
        {
            eOrdinacijaEntities db= new eOrdinacijaEntities();
            var korisnici = from m in db.Korisnik.Where(a=>a.Pacijenti.Count==0 && a.Uposlenik.Count==0)
                            select m;


            if (!String.IsNullOrEmpty(ime))
            {

                korisnici = db.Korisnik.Where(a => a.Ime.Contains(ime) && a.Pacijenti.Count==0 && a.Uposlenik.Count==0);

            }
            return View(korisnici);
        }
        public ActionResult DodajUposlenikaSaAcountom(string id)
        {
            int id_kor = Convert.ToInt32(id);
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Korisnik k = db.Korisnik.Find(id_kor);
            KorisnikUposlenikModel kum= new KorisnikUposlenikModel();
            kum.Korisnik = k;
            ViewBag.uzAcount = "jeste";
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
            return View("DodajUposlenikaSaAcountom", kum);
        
        }
        [HttpPost]
        public ActionResult SacuvajUposlenika(KorisnikUposlenikModel kum) 
        {
          eOrdinacijaEntities db = new eOrdinacijaEntities();
            try{
           db.Uposlenik.Add(new Uposlenik
                    {
                        Biografija = kum.Uposlenik.Biografija,
                        Zanimanje = kum.Uposlenik.Zanimanje,
                        Titula = kum.Uposlenik.Titula,
                        idKorisnika = Convert.ToInt32(kum.Korisnik.IdKorisnika),
                        idOdjela = Convert.ToInt32(kum.Odjel.IdOdjela)
                    }

                    );

                db.SaveChanges();
                return RedirectToAction("HomePage");


            }
            catch (Exception e)
            {
                ViewBag.greska = "Greška , pokušajte ponovo!";
                return View("DodajUposlenikaSaAcountom");

            }
        
        }

        public ActionResult DodajPacijenta() {

            eOrdinacijaEntities db =  new eOrdinacijaEntities();

            ViewBag.sobe = new SelectList(db.Soba, "IdSobe", "brojSobe");
            return View();
        }
        [HttpPost]
        public ActionResult DodajPacijenta(KorisnikPacijentSobaModel k) 
        {
            return RedirectToAction("HomePage");
        }
    }
}
