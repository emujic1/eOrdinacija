//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EOrdinacija_Baze
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pacijenti
    {
        public Pacijenti()
        {
            this.Karton = new HashSet<Karton>();
            this.Nalaz = new HashSet<Nalaz>();
            this.Pregled = new HashSet<Pregled>();
            this.Temini = new HashSet<Temini>();
        }
    
        public int idPacijenta { get; set; }
        public int idKorisnika { get; set; }
        public Nullable<int> idSobe { get; set; }
        public bool Zaposlen { get; set; }
        public string Poslodavac { get; set; }
        public bool Osiguran { get; set; }
        public Nullable<System.DateTime> DatumIstekaOsiguranja { get; set; }
    
        public virtual ICollection<Karton> Karton { get; set; }
        public virtual Korisnik Korisnik { get; set; }
        public virtual ICollection<Nalaz> Nalaz { get; set; }
        public virtual Soba Soba { get; set; }
        public virtual ICollection<Pregled> Pregled { get; set; }
        public virtual ICollection<Temini> Temini { get; set; }
    }
}
