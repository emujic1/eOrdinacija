﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOrdinacija_Baze.Controllers
{
    public class PacijentController : Controller
    {
        //
        // GET: /Pacijent/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult pregledKartona()
        {
            return View();
        }
        public ActionResult ZakaziTermin()
        {
            return View();
        }

    }
}
