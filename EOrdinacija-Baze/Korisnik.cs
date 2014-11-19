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
    
    public partial class Korisnik
    {
        public Korisnik()
        {
            this.Pacijenti = new HashSet<Pacijenti>();
            this.Uposlenik = new HashSet<Uposlenik>();
        }
    
        public int IdKorisnika { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ime_oca { get; set; }
        public string Broj_licne { get; set; }
        public string Email { get; set; }
        public System.DateTime Datum_rodjenja { get; set; }
        public string Mjesto_rodjenja { get; set; }
        public string Prebivalište { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<int> Telefon { get; set; }
        public string JMBG { get; set; }
        public int IdPrivilegije { get; set; }
    
        public virtual Privilegije Privilegije { get; set; }
        public virtual ICollection<Pacijenti> Pacijenti { get; set; }
        public virtual ICollection<Uposlenik> Uposlenik { get; set; }
    }
}
