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

namespace EOrdinacija_Baze.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);

            /*
             * It's possible to use different actions of the current controller
             *      var scheduler = new DHXScheduler(this);     
             *      scheduler.DataAction = "ActionName1";
             *      scheduler.SaveAction = "ActionName2";
             * 
             * Or to specify full paths
             *      var scheduler = new DHXScheduler();
             *      scheduler.DataAction = Url.Action("Data", "Calendar");
             *      scheduler.SaveAction = Url.Action("Save", "Calendar");
             */

            /*
             * The default codebase folder is ~/Scripts/dhtmlxScheduler. It can be overriden:
             *      scheduler.Codebase = Url.Content("~/customCodebaseFolder");
             */

            scheduler.Skin = DHXScheduler.Skins.Terrace;

            scheduler.Config.multi_day = true;

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {

            var data = new SchedulerAjaxData(new KalendarDataContext().Dogaðajs);
            return (ContentResult)data;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var data = new KalendarDataContext();
            try
            {
                var changedEvent = DHXEventsHelper.Bind<Dogaðaj>(actionValues);

                changedEvent.Pocetak = DateTime.Parse(actionValues["start_date"]);
                changedEvent.Kraj = DateTime.Parse(actionValues["end_date"]);
                changedEvent.Opis = (actionValues["text"]).ToString();
                
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        //do insert
                        data.Dogaðajs.InsertOnSubmit(changedEvent);//assign postoperational id
                        break;
                    case DataActionTypes.Delete:
                        //do delete
                        changedEvent = data.Dogaðajs.SingleOrDefault(a => a.Id == action.SourceId);
                        data.Dogaðajs.DeleteOnSubmit(changedEvent);
                        break;
                    default:// "update"          
                        var eventToUpdate = data.Dogaðajs.SingleOrDefault(a => a.Id == action.SourceId);
                        DHXEventsHelper.Update(eventToUpdate, changedEvent, new List<string>() { "id" });

                        //do update
                        break;
                }
                data.SubmitChanges();
                action.TargetId = changedEvent.Id;
            }
            catch
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

