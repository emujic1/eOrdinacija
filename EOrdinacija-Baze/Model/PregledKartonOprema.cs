using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EOrdinacija_Baze
{
    public class PregledKartonOprema
    {
       public Karton Karton { get; set; }
       public Pregled Pregled { get; set; }
       public Image Slika { get; set; }
       public Oprema Oprema { get; set; }
       public Potroseno PotrosenaOprema { get; set; }
    }
}