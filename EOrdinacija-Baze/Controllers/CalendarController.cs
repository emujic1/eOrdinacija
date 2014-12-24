using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using EOrdinacija_Baze.Model;
using System.Globalization;

namespace EOrdinacija_Baze.Controllers
{
    public class CalendarController : Controller
    {
        static int idPacijenta,idUposlenika;
        [HttpGet]
        public ActionResult Index(int id)
        {
            idPacijenta = id;
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes

            return RedirectToAction("PretragaUposlenika");
            
            /*eOrdinacijaEntities db = neweOrdinacijaEntities();
            
             Uposlenik u = db.Uposlenik.Where(a => a.idKorisnika.Equals(id)).FirstOrDefault();

                List<Temini> termini = db.Temini.Where(a => a.idUposlenika.Equals(u.idUposlenika)).ToList();

                var items = new List<object>();
                CultureInfo dutchCulture = CultureInfo.CreateSpecificCulture("nl-NL");

                foreach (var t in termini)
                {

                    string pocetak = String.
             * 
             * at("{0:M/d/yyyy HH:mm:ss}",t.Event.Pocetak);
                    string kraj = String.Format("{0:M/d/yyyy HH:mm:ss}", t.Event.Kraj);

                    items.Add(new { id = t.Event.Id, text = t.Event.Opis, start_date = Convert.ToDateTime(pocetak), end_date = Convert.ToDateTime(kraj) });

                }
                var data = new SchedulerAjaxData(items);

          
            
            var scheduler = new DHXScheduler(this);

            scheduler.Data.Parse(items);

            scheduler.Skin = DHXScheduler.Skins.Terrace;

            scheduler.Config.multi_day = true;

            scheduler.EnableDataprocessor = true;

            return View(scheduler);
             */ 
        }

        public ContentResult Data()
        {

            var data = new SchedulerAjaxData(new KalendarDataContext().Dogaðajs);
            return (ContentResult)data;
        }

        public ActionResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
           KalendarDataContext data = new KalendarDataContext();
            try
            {
                eOrdinacijaEntities db = new eOrdinacijaEntities();
                var changedEvent = DHXEventsHelper.Bind<Dogaðaj>(actionValues, new CultureInfo("en-US"));


                Korisnik k = db.Korisnik.Find(idPacijenta);
                Pacijenti p = db.Pacijenti.Where(a => a.idKorisnika == idPacijenta).FirstOrDefault();
                Uposlenik u = db.Uposlenik.Where(a => a.idKorisnika == idUposlenika).FirstOrDefault();

                            
                changedEvent.Pocetak = DateTime.Parse(actionValues["start_date"]);
                changedEvent.Kraj = DateTime.Parse(actionValues["end_date"]);
                changedEvent.Opis = actionValues["text"].ToString();


                    switch (action.Type)
                    {
                        case DataActionTypes.Insert:
                            //do insert
                            changedEvent.Opis = "Pacijent :" + k.Ime + " (" + k.Ime_oca + ") " + k.Prezime + " \n " + (actionValues["text"]).ToString();

                            data.Dogaðajs.InsertOnSubmit(changedEvent);//assign postoperational id
                            data.SubmitChanges();


                            db.Temini.Add(new Temini
                            {
                                idEventa = changedEvent.Id,
                                idPacijenta = p.idPacijenta,
                                idUposlenika = u.idUposlenika

                            });
                            db.SaveChanges();

                            break;
                        case DataActionTypes.Delete:
                            //do delete
                            changedEvent = data.Dogaðajs.SingleOrDefault(a => a.Id == action.SourceId);
                            data.Dogaðajs.DeleteOnSubmit(changedEvent);

                            Temini t = db.Temini.Where(a => a.idEventa == action.SourceId).FirstOrDefault();
                            db.Temini.Remove(t);
                            db.SaveChanges();
                            data.SubmitChanges();
                            break;
                        default:// "update"          
                            var eventToUpdate = data.Dogaðajs.SingleOrDefault(a => a.Id == action.SourceId);
                            DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });
                            data.SubmitChanges();
                            //do update
                            break;
                    }
              
                
                action.TargetId = changedEvent.Id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }

        public ActionResult PretragaUposlenika(string ime,int? id) {

           eOrdinacijaEntities db = new eOrdinacijaEntities();
          
               var uposlenici = from m in db.Korisnik.Where(a => a.Pacijenti.Count == 0 && a.Uposlenik.Count > 0)
                                select m;


               if (!String.IsNullOrEmpty(ime))
               {

                   uposlenici = db.Korisnik.Where(a => a.Ime.Contains(ime) && a.Pacijenti.Count == 0 && a.Uposlenik.Count > 0);

               }
               return View("Index", uposlenici);
           }
        public ActionResult PrikaziKalendar(int id)

        {
               idUposlenika = id;
             eOrdinacijaEntities db = new eOrdinacijaEntities();

               Uposlenik u = db.Uposlenik.Where(a => a.idKorisnika.Equals(id)).FirstOrDefault();

               List<Temini> termini = db.Temini.Where(a => a.idUposlenika==u.idUposlenika).ToList();

               var items = new List<object>();
               CultureInfo dutchCulture = CultureInfo.CreateSpecificCulture("nl-NL");

               foreach (var t in termini)
               {

               //    string pocetak = String.Format("{0:M/d/yyyy HH:mm:ss}", t.Event.Pocetak);
                 //  string kraj = String.Format("{0:M/d/yyyy HH:mm:ss}", t.Event.Kraj);

                   items.Add(new { id = t.Event.Id, text = t.Event.Opis, start_date = t.Event.Pocetak, end_date = t.Event.Kraj });

               }
               var data = new SchedulerAjaxData(items);



               var scheduler = new DHXScheduler(this);

               scheduler.Data.Parse(items);

               scheduler.Config.first_hour = 7;
               scheduler.Config.last_hour = 21;
               scheduler.Config.xml_date = "%d/%m/%Y %H:%i";
           
               scheduler.EnableDataprocessor = true;
            
            
              
               return View("PrikaziKalendar",scheduler);
       
        }



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

        
    }
}

