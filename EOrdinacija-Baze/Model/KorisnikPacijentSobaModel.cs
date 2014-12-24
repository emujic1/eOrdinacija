using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EOrdinacija_Baze
{
    public class KorisnikPacijentSobaModel
    {
        public Korisnik Korisnik { get; set; }
        public Soba Soba { get; set; }
        public Pacijenti Pacijent { get; set; }

        public List<Soba> Sobe { get; set; }
    }
}