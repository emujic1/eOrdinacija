using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DHTMLX.Scheduler.Data;
using EOrdinacija_Baze;
using System.Globalization;

namespace eOrdinacija_Baze.Controllers
{
    public class KorisnikController : Controller
    {
         static int idKorisnika;

         public ActionResult Onama() {
             return PartialView("Onama");
         }
        
        public ActionResult DajKarton()
         {
             eOrdinacijaEntities db = new eOrdinacijaEntities();
             Pacijenti p = db.Pacijenti.Where(a => a.idKorisnika == idKorisnika).FirstOrDefault();
             Karton k = db.Karton.Where(a => a.IdPacijenta == p.idPacijenta).FirstOrDefault();
             if (k != null)
                 return View("PregledajKarton", k);
             else
             {
                 string poruka = @"alert('Vas karton je prazan');
                  window.location.reload()";
                 return JavaScript(poruka);
             }
         }

        public ActionResult HomePage() {

            ViewBag.dodanDoktor = TempData["DodanUposlenik"];


           
            bool add_doktor = false,
            add_pacijent = false,
            del_doktor = false,
            izm_priv = false,
            addTermin = false,
            doktor = false,
            add_oprema = false;

            eOrdinacijaEntities dc = new eOrdinacijaEntities();

            Korisnik k = dc.Korisnik.Find(idKorisnika);
            


            if (k.Privilegije.add_doktor)
            {
                add_doktor = true;
            }
            if (k.Privilegije.add_pacijent)
            {
                add_pacijent = true;
            }
            if (k.Privilegije.del_doktor)
            {
                del_doktor = true;
            }
            if (k.Privilegije.add_new_privilegije)
            {
                izm_priv = true;
            }
            if (k.Privilegije.zakazivanje_termina)
            {
                addTermin = true;
            }
            if (k.Privilegije.ažuriranje_opreme)
            {
                add_oprema = true;
            }
            if (k.Pacijenti.Count > 0) {
                ViewBag.pacijent = "Trenutno je pacijent";
            }
            if (k.Pacijenti.Count == 0 && k.Uposlenik.Count == 0) 
            {
                ViewBag.korisnik = "Trenutno je korisnik";
            }
            if (k.Uposlenik.Count != 0)
            {
                var scheduler = new DHXScheduler(this);


                doktor = true;
                Uposlenik u = dc.Uposlenik.Where(a => a.idKorisnika.Equals(idKorisnika)).FirstOrDefault();

                List<Temini> termini = dc.Temini.Where(a => a.idUposlenika == u.idUposlenika).ToList();

                var items = new List<object>();

                foreach (var t in termini)
                {


                    items.Add(new { id = t.Event.Id, text = t.Event.Opis, start_date = t.Event.Pocetak, end_date = t.Event.Kraj });

                }
                var data = new SchedulerAjaxData(items);

                scheduler.Data.Parse(items);
                scheduler.Skin = DHXScheduler.Skins.Terrace;

                scheduler.Config.multi_day = true;

                scheduler.Config.drag_move = false;
                scheduler.Config.drag_resize = false;
                
                scheduler.Config.first_hour = 7;
                scheduler.Config.last_hour = 21;
                scheduler.Config.xml_date = "%d/%m/%Y %H:%i";

                scheduler.EnableDataprocessor = true;
                scheduler.Config.buttons_right = new LightboxButtonList {
            LightboxButtonList.Cancel
        };
                scheduler.Config.buttons_left = new LightboxButtonList{
                new EventButton
                {
                    Label = " Izvrši pregled ",
                    OnClick = "Izvrši",
                    Name = "Izvršipregled"
                },
                 new EventButton
                {
                    Label = " Pregledaj karton pacijenta ",
                    OnClick = "PregledKartona",
                    Name = "PregledKartona"
                }
                };
                scheduler.Config.icons_select = new EventButtonList
                {

                };
                ViewBag.add_doktor = add_doktor;
                ViewBag.add_pacijent = add_pacijent;
                ViewBag.del_doktor = del_doktor;
                ViewBag.izm_priv = izm_priv;
                ViewBag.addTermin = addTermin;
                ViewBag.doktor = doktor;
                ViewBag.add_oprema = add_oprema;
                return View(scheduler);

            }
            
                ViewBag.add_doktor = add_doktor;
                ViewBag.add_pacijent = add_pacijent;
                ViewBag.del_doktor = del_doktor;
                ViewBag.izm_priv = izm_priv;
                ViewBag.addTermin = addTermin;
                ViewBag.doktor = doktor;
                ViewBag.add_oprema = add_oprema;
                    
                return View();
                
        }
        public ActionResult DajRasporedPacijenta() 
        {
         
            var scheduler = new DHXScheduler(this);


                eOrdinacijaEntities dc =new eOrdinacijaEntities();

                Pacijenti u = dc.Pacijenti.Where(a => a.idKorisnika.Equals(idKorisnika)).FirstOrDefault();

                List<Temini> termini = dc.Temini.Where(a => a.idPacijenta == u.idPacijenta).ToList();

                var items = new List<object>();

                foreach (var t in termini)
                {


                    items.Add(new { id = t.Event.Id, text = t.Event.Opis, start_date = t.Event.Pocetak, end_date = t.Event.Kraj });

                }
                var data = new SchedulerAjaxData(items);

                scheduler.Data.Parse(items);
                scheduler.Skin = DHXScheduler.Skins.Terrace;

                scheduler.Config.multi_day = true;

                scheduler.Config.drag_move = false;
                scheduler.Config.drag_resize = false;
                
                scheduler.Config.first_hour = 7;
                scheduler.Config.last_hour = 21;
                scheduler.Config.xml_date = "%d/%m/%Y %H:%i";

                scheduler.EnableDataprocessor = false;
                scheduler.Config.buttons_right = new LightboxButtonList {
            LightboxButtonList.Cancel
        };
                scheduler.Config.buttons_left = new LightboxButtonList{
                
                };
                scheduler.Config.icons_select = new EventButtonList
                {

                };              
                    
                return PartialView(scheduler);
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
            if (kum.Korisnik.Datum_rodjenja > DateTime.Now)
            {
                ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela"); 
                return View("DodajUposlenika");

            }
            var user = db.Korisnik.Where(a => a.Username.Contains(kum.Korisnik.Username)).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("CustomError1", "Username vec postoji");
                ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela"); 
                return View("DodajUposlenika");

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
                imePrivilegije = kum.Korisnik.Username
            });
            db.SaveChanges();
            Privilegije p = db.Privilegije.Where(a => a.imePrivilegije == kum.Korisnik.Username).FirstOrDefault();

            try
            {
                 

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
                    idPrivilegije = p.idPrivilegije,
                    JMBG = kum.Korisnik.JMBG,
                    Password = kum.Korisnik.Password,
                    Username = kum.Korisnik.Username,
                    Telefon = kum.Korisnik.Telefon
                });

                db.SaveChanges();

                var c = db.Korisnik.Where(a => a.Username.Equals(kum.Korisnik.Username)).FirstOrDefault();
                int idKor = c.IdKorisnika;

                try
                {

                    db.Uposlenik.Add(new Uposlenik
                        {
                            Biografija = kum.Uposlenik.Biografija,
                            Zanimanje = kum.Uposlenik.Zanimanje,
                            Titula = kum.Uposlenik.Titula,
                            idKorisnika = idKor,
                            idOdjela = Convert.ToInt32(kum.Odjel.IdOdjela)
                        }

                        );

                    db.SaveChanges();
                }
                catch (Exception e) {
                    db.Korisnik.Remove(db.Korisnik.Find(kum.Korisnik.IdKorisnika));
                    db.SaveChanges();
                    ViewBag.greska = "Doslo je do greske, pokusajte ponovo !";
                    return View("DodajUposlenika");
                
                }

            }
            catch (Exception e)
            {
                ViewBag.greska = "Doslo je do greske, pokusajte ponovo !";
                return View("DodajUposlenika");

            }
            TempData["DodanUposlenik"] = "Uposlenik uspješno spašen";
            return RedirectToAction("HomePage");
        }
        public ActionResult BrišiPacijenta(string ime) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();

            var korisnici = from m in db.Korisnik.Where(a => a.Pacijenti.Count > 0)
                            select m;


            if (!String.IsNullOrEmpty(ime))
            {

                korisnici = db.Korisnik.Where(a => a.Ime.Contains(ime) && a.Pacijenti.Count > 0);

            }
            return View(korisnici);
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
        public ActionResult ObrišiKorisnikaPacijenta(int id) {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            if (db.Korisnik.Find(idKorisnika).Privilegije.del_pacijent == false)
            {
                TempData["Greska"] = "Žao nam je , nemate privilegiju brisanja pacijenta";
                return RedirectToAction("DodajPacijenta");
            }


            try
            {
                Korisnik o = db.Korisnik.Find(id);
                Pacijenti uposlenik = db.Pacijenti.Where(a => a.idKorisnika.Equals(id)).FirstOrDefault();

                if (uposlenik.Pregled.Count > 0)
                {
                    TempData["Greska"] = "Pacijent ima zakazane termine!";
                    return RedirectToAction("DodajPacijenta");
                }
                db.Pacijenti.Remove(uposlenik);
                db.Korisnik.Remove(o);
                db.SaveChanges();
                TempData["KorisnikIzmjenjen"] = "Pacijent uspješno obrisan";
                return RedirectToAction("DodajPacijenta");
            }
            catch (Exception e)
            {

                return RedirectToAction("BrišiPacijenta");
            }
        }
        public ActionResult ObrišiKorisnikaiUposlenika(int id)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            if (db.Korisnik.Find(idKorisnika).Privilegije.del_doktor == false)
            {
                TempData["KorisnikIzmjenjen"] = "Žao nam je , nemate privilegiju brisanja uposlenika";
                return RedirectToAction("DodajUposlenika");
            }

            
            try
            {
                Korisnik o = db.Korisnik.Find(id);
                Uposlenik uposlenik = db.Uposlenik.Where(a => a.idKorisnika.Equals(id)).FirstOrDefault();

                if (uposlenik.Pregled.Count > 0) 
                {
                    TempData["Upozorenje"] = "Uposlenik ima zakazane termine!";
                    return RedirectToAction("DodajUposlenika");
                }
                db.Uposlenik.Remove(uposlenik);
                db.Korisnik.Remove(o);              
                db.SaveChanges();
                TempData["KorisnikIzmjenjen"] = "Uposlenik uspješno obrisan";
                return RedirectToAction("DodajUposlenika");
            }
            catch (Exception e) {

                return RedirectToAction("BrišiUposlenika");
            }
        }
        public ActionResult IzmjeniKorisnikaPacijenta(int id)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            if (db.Korisnik.Find(idKorisnika).Privilegije.modify_pacijent == false)
            {
                TempData["Greska"] = "Žao nam je , nemate privilegiju izmjene uposlenika";
                return RedirectToAction("DodajPacijenta");
            }

            Korisnik o = db.Korisnik.Find(id);
            Pacijenti uposlenik = db.Pacijenti.Where(a => a.idKorisnika.Equals(id)).FirstOrDefault();
           
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
         //   o.Datum_rodjenja = DateTime.ParseExact(o.Datum_rodjenja.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            KorisnikPacijentSobaModel kum = new KorisnikPacijentSobaModel();
            kum.Korisnik = o;
            kum.Pacijent = uposlenik;
          
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
            string[] jedinice = new string[] { "Odaberi" };
            ViewBag.sobe = new SelectList(jedinice);
            kum.Sobe = db.Soba.ToList();

            return View("IzmjeniKorisnikaPacijenta", kum);
        }
        [HttpPost]
        public ActionResult SpremiIzmjenjenogPacijenta(KorisnikPacijentSobaModel kum) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            if (kum.Korisnik.Datum_rodjenja > DateTime.Now)
            {
                ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
                return View("IzmjeniKorisnikaPacijenta");

            }
            var user = db.Korisnik.Where(a => a.Username == kum.Korisnik.Username && a.IdKorisnika != kum.Korisnik.IdKorisnika).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("CustomError1", "Username vec postoji");
                ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
                return View("IzmjeniKorisnikaPacijenta");

            }

            Korisnik k = db.Korisnik.Find(kum.Korisnik.IdKorisnika);
            Pacijenti u = db.Pacijenti.Find(kum.Pacijent.idPacijenta);



            k.Ime = kum.Korisnik.Ime;
            k.Prezime = kum.Korisnik.Prezime;
            k.Ime_oca = kum.Korisnik.Ime_oca;
            k.Broj_licne = kum.Korisnik.Broj_licne;
            k.Email = kum.Korisnik.Email;
            k.Datum_rodjenja = kum.Korisnik.Datum_rodjenja;
            k.Mjesto_rodjenja = kum.Korisnik.Mjesto_rodjenja;
            k.Prebivalište = kum.Korisnik.Prebivalište;
            k.Username = kum.Korisnik.Username;
            k.Password = kum.Korisnik.Password;
            k.Telefon = kum.Korisnik.Telefon;
            k.JMBG = kum.Korisnik.JMBG;
            k.idPrivilegije = kum.Korisnik.idPrivilegije;

            u.idSobe = Convert.ToInt32(kum.Soba.IdSobe);
            u.Zaposlen = kum.Pacijent.Zaposlen;
            u.Poslodavac = kum.Pacijent.Poslodavac;
            u.Osiguran = kum.Pacijent.Osiguran;

            db.SaveChanges();

            TempData["KorisnikIzmjenjen"] = "Korisnik uspješno izmjenjen";
            return RedirectToAction("DodajPacijenta");
        }
        public ActionResult IzmjeniKorisnikaiUposlenika(int id)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            if (db.Korisnik.Find(idKorisnika).Privilegije.modify_doktor == false) 
            {
                TempData["KorisnikIzmjenjen"] = "Žao nam je , nemate privilegiju izmjene uposlenika";
                return RedirectToAction("DodajUposlenika");
            }
            
            Korisnik o = db.Korisnik.Find(id);
            Uposlenik uposlenik = db.Uposlenik.Where(a => a.idKorisnika.Equals(id)).FirstOrDefault();
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela",uposlenik.idOdjela);

            KorisnikUposlenikModel kum = new KorisnikUposlenikModel();
            kum.Korisnik = o;
            kum.Uposlenik = uposlenik;
            return View("IzmjeniKorisnikaiUposlenika", kum);
        
        }
        public ActionResult IzmjeniUposlenika(KorisnikUposlenikModel kum) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            if (kum.Korisnik.Datum_rodjenja > DateTime.Now)
            {
                ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela",kum.Uposlenik.idOdjela);
                return View("IzmjeniKorisnikaiUposlenika");

            }
            var user = db.Korisnik.Where(a => a.Username ==kum.Korisnik.Username && a.IdKorisnika != kum.Korisnik.IdKorisnika).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("CustomError1", "Username vec postoji");
                ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela",kum.Uposlenik.idOdjela);
                return View("IzmjeniKorisnikaiUposlenika");

            }

            Korisnik k = db.Korisnik.Find(kum.Korisnik.IdKorisnika);
            Uposlenik u = db.Uposlenik.Find(kum.Uposlenik.idUposlenika);

            

            k.Ime = kum.Korisnik.Ime;
            k.Prezime = kum.Korisnik.Prezime;
            k.Ime_oca = kum.Korisnik.Ime_oca;
            k.Broj_licne = kum.Korisnik.Broj_licne;
            k.Email = kum.Korisnik.Email;
            k.Datum_rodjenja = kum.Korisnik.Datum_rodjenja;
            k.Mjesto_rodjenja = kum.Korisnik.Mjesto_rodjenja;
            k.Prebivalište = kum.Korisnik.Prebivalište;
            k.Username = kum.Korisnik.Username;
            k.Password = kum.Korisnik.Password;
            k.Telefon = kum.Korisnik.Telefon;
            k.JMBG = kum.Korisnik.JMBG;
            k.idPrivilegije = kum.Korisnik.idPrivilegije;

            u.idOdjela = kum.Odjel.IdOdjela;
            u.Titula = kum.Uposlenik.Titula;
            u.Zanimanje = kum.Uposlenik.Zanimanje;
            u.Biografija = kum.Uposlenik.Biografija;

            db.SaveChanges();

            TempData["KorisnikIzmjenjen"] = "Korisnik uspješno izmjenjen";
            return RedirectToAction("DodajUposlenika");
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
        public ActionResult IzmjenaPrivilegije(int id) {
            
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Korisnik k = db.Korisnik.Find(id);
            Privilegije p = db.Privilegije.Where(a => a.idPrivilegije == k.idPrivilegije).FirstOrDefault();
            return View(p);
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
        public ActionResult dodajPacijentasaAcountom(string ime)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            var korisnici = from m in db.Korisnik.Where(a => a.Pacijenti.Count == 0 && a.Uposlenik.Count == 0)
                            select m;


            if (!String.IsNullOrEmpty(ime))
            {

                korisnici = db.Korisnik.Where(a => a.Ime.Contains(ime) && a.Pacijenti.Count == 0 && a.Uposlenik.Count == 0);

            }
            return View("dodajPacijentasaAcountom", korisnici);
        }
        public ActionResult PacijentaSaAcountom(int id) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Korisnik k = db.Korisnik.Find(id);
            KorisnikPacijentSobaModel kum = new KorisnikPacijentSobaModel();
            kum.Korisnik = k;
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
            string[] jedinice = new string[] { "Odaberi" };
            ViewBag.sobe = new SelectList(jedinice);
            kum.Sobe = db.Soba.ToList();
            return View("PacijentaSaAcountom", kum);
        }
        [HttpPost]
        public ActionResult SpremiPacijentaSaAcountom(KorisnikPacijentSobaModel kum) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            try
            {
                if (kum.Korisnik.Datum_rodjenja > DateTime.Now)
                {
                    ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                    ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
                    return View("PacijentaSaAcountom", kum);

                }
                var user = db.Korisnik.Where(a => a.Username == kum.Korisnik.Username && a.IdKorisnika != kum.Korisnik.IdKorisnika).FirstOrDefault();

                if (user != null)
                {
                    ModelState.AddModelError("CustomError1", "Username vec postoji");
                    ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
                    return View("PacijentaSaAcountom", kum);

                }

                Korisnik k = db.Korisnik.Find(kum.Korisnik.IdKorisnika);
                k.Broj_licne = kum.Korisnik.Broj_licne;
                k.Datum_rodjenja = kum.Korisnik.Datum_rodjenja;
                k.Email = kum.Korisnik.Email;
                k.Ime = kum.Korisnik.Ime;
                k.Ime_oca = kum.Korisnik.Ime_oca;
                k.JMBG = kum.Korisnik.JMBG;
                k.Mjesto_rodjenja = kum.Korisnik.Mjesto_rodjenja;
                k.Password = kum.Korisnik.Password;
                k.Prebivalište = kum.Korisnik.Prebivalište;
                k.Prezime = kum.Korisnik.Prezime;
                k.Telefon = kum.Korisnik.Telefon;
                k.Username = kum.Korisnik.Username;
                k.idPrivilegije = kum.Korisnik.idPrivilegije;
                db.SaveChanges();


                db.Pacijenti.Add(new Pacijenti
                {
                    Zaposlen = kum.Pacijent.Zaposlen,
                    Poslodavac = kum.Pacijent.Poslodavac,
                    Osiguran = kum.Pacijent.Osiguran,
                    idKorisnika = Convert.ToInt32(kum.Korisnik.IdKorisnika),
                    idSobe = Convert.ToInt32(kum.Soba.IdSobe)
                });
                db.SaveChanges();

                TempData["DodanUposlenik"] = "Pacijent uspješno spašen";

                return RedirectToAction("HomePage");


            }
            catch (Exception e)
            {
                ViewBag.greska = "Greška , pokušajte ponovo!";
                return View("dodajPacijentasaAcountom");

            }
        
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
        public ActionResult DodajUposlenikaSaAcountom(int id)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Korisnik k = db.Korisnik.Find(id);
            KorisnikUposlenikModel kum= new KorisnikUposlenikModel();
            kum.Korisnik = k;
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
            return View("DodajUposlenikaSaAcountom", kum);
        
        }
        [HttpPost]
        public ActionResult SacuvajUposlenika(KorisnikUposlenikModel kum) 
        {
        
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            try{
                if (kum.Korisnik.Datum_rodjenja > DateTime.Now)
                {
                    ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                    ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
                    return View("DodajUposlenikaSaAcountom", kum);

                }
                var user = db.Korisnik.Where(a => a.Username == kum.Korisnik.Username && a.IdKorisnika != kum.Korisnik.IdKorisnika).FirstOrDefault();

                if (user != null)
                {
                    ModelState.AddModelError("CustomError1", "Username vec postoji");
                    ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
                    return View("DodajUposlenikaSaAcountom", kum);

                }
                
                Korisnik k = db.Korisnik.Find(kum.Korisnik.IdKorisnika);
                k.Broj_licne = kum.Korisnik.Broj_licne;
                k.Datum_rodjenja = kum.Korisnik.Datum_rodjenja;
                k.Email = kum.Korisnik.Email;
                k.Ime = kum.Korisnik.Ime;
                k.Ime_oca = kum.Korisnik.Ime_oca;
                k.JMBG = kum.Korisnik.JMBG;
                k.Mjesto_rodjenja = kum.Korisnik.Mjesto_rodjenja;
                k.Password = kum.Korisnik.Password;
                k.Prebivalište = kum.Korisnik.Prebivalište;
                k.Prezime = kum.Korisnik.Prezime;
                k.Telefon = kum.Korisnik.Telefon;
                k.Username = kum.Korisnik.Username;
                db.SaveChanges();
                
                
                db.Uposlenik.Add(new Uposlenik
                    {
                        Biografija = kum.Uposlenik.Biografija,
                        Zanimanje = kum.Uposlenik.Zanimanje,
                        Titula = kum.Uposlenik.Titula,
                        idKorisnika = Convert.ToInt32(kum.Korisnik.IdKorisnika),
                        idOdjela = Convert.ToInt32(kum.Odjel.IdOdjela)
                    });
                db.SaveChanges();

                TempData["DodanUposlenik"] = "Uposlenik uspješno spašen";

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
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");
            string[] jedinice = new string[] { "Odaberi" };
            ViewBag.sobe = new SelectList(jedinice);

            KorisnikPacijentSobaModel kum = new KorisnikPacijentSobaModel();
            kum.Sobe = db.Soba.ToList();
            return View(kum);
        }
        [HttpPost]
        public ActionResult DodajPacijenta(KorisnikPacijentSobaModel kum) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();

            if (kum.Korisnik.Datum_rodjenja > DateTime.Now) {
                ModelState.AddModelError("CustomError", "Datum mora biti u budučnosti");
                ViewBag.sobe = new SelectList(db.Soba, "IdSobe", "brojSobe");
                return View("DodajPacijenta");
            
            }
            var user = db.Korisnik.Where(a => a.Username.Contains(kum.Korisnik.Username)).FirstOrDefault();

            if (user != null) {
                ModelState.AddModelError("CustomError1", "Username vec postoji");
                ViewBag.sobe = new SelectList(db.Soba, "IdSobe", "brojSobe");
                return View("DodajPacijenta");
            
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
                imePrivilegije = kum.Korisnik.Username
            });
            db.SaveChanges();
            Privilegije p = db.Privilegije.Where(a => a.imePrivilegije == kum.Korisnik.Username).FirstOrDefault();

        try{
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
                    idPrivilegije = p.idPrivilegije,
                    JMBG = kum.Korisnik.JMBG,
                    Password = kum.Korisnik.Password,
                    Username = kum.Korisnik.Username,
                    Telefon = kum.Korisnik.Telefon
                });

                db.SaveChanges();

                var c = db.Korisnik.Where(a => a.Username.Equals(kum.Korisnik.Username)).FirstOrDefault();
                int idKor = c.IdKorisnika;
             try
                {

                    db.Pacijenti.Add(new Pacijenti
                        {
                            idKorisnika = idKor,
                            idSobe = Convert.ToInt32(kum.Soba.IdSobe),
                            Zaposlen = kum.Pacijent.Zaposlen,
                            Poslodavac = kum.Pacijent.Poslodavac,
                            Osiguran = kum.Pacijent.Osiguran
                        }

                        );

                    db.SaveChanges();
                }
                catch (Exception e) {
                    db.Korisnik.Remove(db.Korisnik.Find(kum.Korisnik.IdKorisnika));
                    db.SaveChanges();
                    ViewBag.greska = "Doslo je do greske, pokusajte ponovo !";
                    return View("DodajPacijenta");
                
                }

            }
            catch (Exception e)
            {
                ViewBag.greska = "Doslo je do greske, pokusajte ponovo !";
                return View("DodajPacijenta");

            }
        TempData["DodanUposlenik"] = "Pacijent uspješno spašen";

            return RedirectToAction("HomePage");
            
        }
        public ActionResult AddTermin(string ime) {
            
            eOrdinacijaEntities db = new eOrdinacijaEntities();

            var pacijenti = from m in db.Korisnik.Where(a => a.Pacijenti.Count > 0 && a.Uposlenik.Count == 0)
                            select m;


            if (!String.IsNullOrEmpty(ime))
            {

                pacijenti = db.Korisnik.Where(a => a.Ime.Contains(ime) && a.Pacijenti.Count > 0 && a.Uposlenik.Count == 0);

            }
 
            return View("izaberiZaTermin",pacijenti);
        }

        [HttpGet]
        public ActionResult UradiPregled(int id)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Temini t = db.Temini.Where(a => a.idEventa == id).FirstOrDefault();
            PregledKartonOprema pko = new PregledKartonOprema();

            pko.Karton = new Karton();
            pko.Pregled = new Pregled();
                
            pko.Karton.IdPacijenta = Convert.ToInt32(t.idPacijenta);
            pko.Pregled.idPacijenta = Convert.ToInt32(t.idPacijenta);
            pko.Pregled.IdUposlenika = Convert.ToInt32(t.idUposlenika);
            ViewBag.oprema = new SelectList(db.Oprema, "IdOpreme", "Naziv");
            string[] jedinice = new string[] { "mg", "dg", "kg"};
            ViewBag.jedinice = new SelectList(jedinice, "mg");
            
            return View("IzvršiPregled",pko);
        }
        [HttpPost]
        public ActionResult SavePregled(PregledKartonOprema pko)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();

            if (pko.Slika.File != null)
            {
            
            pko.Slika.FileName = pko.Slika.File.FileName;
            pko.Slika.ImageSize = pko.Slika.File.ContentLength;

            byte[] data = new byte[pko.Slika.File.ContentLength];
            pko.Slika.File.InputStream.Read(data, 0, pko.Slika.File.ContentLength);
            pko.Slika.ImageData = data;

            
                db.Image.Add(pko.Slika);
                db.SaveChanges();

                Image i = db.Image.Where(a => a.ImageData == pko.Slika.ImageData).FirstOrDefault();

                db.Karton.Add(new Karton
                {
                    IdPacijenta = pko.Pregled.idPacijenta,
                    Misljenje_specijaliste = pko.Karton.Misljenje_specijaliste,
                    Datum_zadnje_ismjene = DateTime.Now,
                    Dijagnoza = pko.Karton.Dijagnoza,
                    Primjedba = pko.Karton.Primjedba,
                    Sposobnost_kretanja = pko.Karton.Sposobnost_kretanja,
                    Zarazna_bolest = pko.Karton.Zarazna_bolest,
                    Slika = i.ImageId

                });
            }
            else {
                db.Karton.Add(new Karton
                {
                    IdPacijenta = pko.Pregled.idPacijenta,
                    Misljenje_specijaliste = pko.Karton.Misljenje_specijaliste,
                    Datum_zadnje_ismjene = DateTime.Now,
                    Dijagnoza = pko.Karton.Dijagnoza,
                    Primjedba = pko.Karton.Primjedba,
                    Sposobnost_kretanja = pko.Karton.Sposobnost_kretanja,
                    Zarazna_bolest = pko.Karton.Zarazna_bolest                   

                });
            
            }
              
            Uposlenik k = db.Uposlenik.Where(a => a.idKorisnika == idKorisnika).FirstOrDefault();

            db.Potroseno.Add(new Potroseno
            {
                Oprema = pko.Oprema.IdOpreme,
                Kolićina = pko.PotrosenaOprema.Kolićina,
                Jedinica = pko.PotrosenaOprema.Jedinica
            });
            db.SaveChanges();

            Potroseno pot = db.Potroseno.Where(a=>a.Oprema == pko.Oprema.IdOpreme && a.Kolićina==pko.PotrosenaOprema.Kolićina && a.Jedinica == pko.PotrosenaOprema.Jedinica).FirstOrDefault();
            
            db.Pregled.Add(new Pregled { 
            Datum = DateTime.Now ,
            Razlog_posjete = pko.Pregled.Razlog_posjete,
            Simptomi = pko.Pregled.Simptomi,
            Oprema = pot.IdPotoseno,
            idPacijenta = pko.Pregled.idPacijenta,
            IdUposlenika = k.idUposlenika
            });
         
            db.SaveChanges();
          
            
            return RedirectToAction("HomePage");
        }

        [HttpPost]
        public ActionResult PregledajKarton(int id) {

            eOrdinacijaEntities db = new eOrdinacijaEntities();
            Temini t = db.Temini.Where(a => a.idEventa == id).FirstOrDefault();
            Karton k = db.Karton.Where(a => a.IdPacijenta == t.idPacijenta).FirstOrDefault();
            Korisnik pacijent = db.Korisnik.Where(a => a.IdKorisnika == db.Pacijenti.Where(b => b.idPacijenta == t.idPacijenta).FirstOrDefault().idKorisnika).FirstOrDefault();
            ViewBag.pacijent = pacijent.Ime +" (" + pacijent.Ime_oca +") "+ pacijent.Prezime;
            if (k == null)
            {
                string poruka = @"alert('Karton je prazan');
                window.location.href = 'HomePage'";
                return JavaScript(poruka);
            }
            else
            {

                return View(k);
            }
        }
        public ActionResult VidiSliku(int id) {

            eOrdinacijaEntities db = new eOrdinacijaEntities();          
            return View(db.Image.Find(id));
        }
        public ActionResult dodajOpremu()         
        {            
            return View();
        }
        [HttpPost]
        public ActionResult DodajSobu() {
            eOrdinacijaEntities db= new eOrdinacijaEntities();
            ViewBag.odjeli = new SelectList(db.Odjel, "IdOdjela", "TipOdjela");

            return View();
        }
        [HttpPost]
        public ActionResult SaveSoba(Soba s)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            
                db.Soba.Add(s);
                db.SaveChanges();
                return RedirectToAction("dodajOpremu");
           
        }
        [HttpPost]
        public ActionResult DodajOdjel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SpremiOdjel(Odjel o)
        {

            eOrdinacijaEntities db = new eOrdinacijaEntities();               
            db.Odjel.Add(o);
            db.SaveChanges();
            return RedirectToAction("dodajOpremu");
        }
        
             public ActionResult DodajLijek()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SpremiLijek(Oprema l)
        {

            eOrdinacijaEntities db = new eOrdinacijaEntities();
                
            db.Oprema.Add(l);
            db.SaveChanges();
            return RedirectToAction("dodajOpremu");
        }
        public JsonResult ProvjeriSobe(int idOdjela)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            int k = db.Soba.Count(a =>a.idOdjela == idOdjela);
            Odjel o = db.Odjel.Find(idOdjela);


            return Json((k <= o.BrojSoba), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProvjeraTipaOdjela(string TipOdjela)
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
            return Json(!db.Odjel.Any(a => a.TipOdjela == TipOdjela), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult PotroseniLijek() {
           eOrdinacijaEntities db =new eOrdinacijaEntities();
           ViewBag.lijekovi = new SelectList(db.Oprema , "idOpreme" , "Naziv");
            return View();
        }
        public ActionResult DajLijek(int id)        
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
          
            DateTime jucer = DateTime.Today.AddDays(-1); 
            DateTime prijSedmicu = DateTime.Today.AddDays(-7);
            DateTime prijeMjesec = DateTime.Today.AddMonths(-1);
            DateTime prijeGodinu = DateTime.Today.AddYears(-1);
            
            double sumajucer=0, sumamjesec=0, sumagodina=0, sumasedmica=0;
           

            List<Potroseno> potroseniLijek = db.Potroseno.Where(a => a.Oprema == id).ToList();

            List<Pregled> pregledijucer = new List<Pregled>();
            List<Pregled> preglediSedmicu = new List<Pregled>();
            List<Pregled> preglediMjesec = new List<Pregled>();
            List<Pregled> preglediGodinu = new List<Pregled>();

            foreach (var p in potroseniLijek)
            {
                var c = db.Pregled.Where(a => a.Oprema == p.IdPotoseno).FirstOrDefault();
                if (c != null)
                {
                    if (c.Datum > jucer)
                    {
                        pregledijucer.Add(c);
                    }
                    if (c.Datum > prijSedmicu)
                    {
                        preglediSedmicu.Add(c);
                    }
                    if (c.Datum > prijeMjesec)
                    {
                        preglediMjesec.Add(c);
                    }
                    if (c.Datum > prijeGodinu)
                    {
                        preglediGodinu.Add(c);
                    }
                }
            }
            
            foreach(var p in pregledijucer)
            {
               
                    
                        double k = Convert.ToDouble(p.Potroseno.Kolićina);
                        if (p.Potroseno.Jedinica == "kg") k *= 1000;
                        else
                        if (p.Potroseno.Jedinica == "dg") k = 10;
                            
                         sumajucer += k;
                    
                
            }
            foreach (var p in preglediSedmicu)
            {
                
                        double k = Convert.ToDouble(p.Potroseno.Kolićina);
                        if (p.Potroseno.Jedinica == "kg") k *= 1000;
                        else
                            if (p.Potroseno.Jedinica == "dg") k *= 10;
                            
                                sumasedmica += k;
                 
            }
            foreach (var p in preglediMjesec)
            {
                
                        double k = Convert.ToDouble(p.Potroseno.Kolićina);
                        if (p.Potroseno.Jedinica == "kg") k *= 1000;
                        else
                            if (p.Potroseno.Jedinica == "dg") k *= 10;
                            
                                sumamjesec += k;
                  
            }
            foreach (Pregled p in preglediGodinu)
            {
                
                        double k = Convert.ToDouble(p.Potroseno.Kolićina);
                        if (p.Potroseno.Jedinica == "kg") k *= 1000;
                        else
                            if (p.Potroseno.Jedinica == "dg") k *= 10;
                            
                                sumagodina += k;
                  
            }

            ViewBag.jucer = sumajucer;
            ViewBag.sedmica = sumasedmica;
            ViewBag.mjesec = sumamjesec;
            ViewBag.godina = sumagodina;

                return View();
        }

        private bool CheckNullOrEmpty(int p)
        {
            if (p != null || p == 0) return true;
            else return false;
        }

        public void DajSobe(int id) 
        {
            eOrdinacijaEntities db = new eOrdinacijaEntities();
           TempData["sobe"] = db.Soba.Where(a=>a.idOdjela==id).ToList();
            

        }
    }

}

