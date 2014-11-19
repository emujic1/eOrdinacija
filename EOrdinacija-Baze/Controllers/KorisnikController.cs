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
        [HttpGet]
        public ActionResult Index(int id)
        {
            eOrdinacijaEntities dc = new eOrdinacijaEntities();
            var tmp = dc.Korisnik.Where(a => a.IdKorisnika.Equals(id)).FirstOrDefault();
            if (tmp.Privilegije.add_doktor)
            {
                return View();
            }
            return View();
               
        }

    }
}
