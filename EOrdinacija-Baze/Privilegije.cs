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
    
    public partial class Privilegije
    {
        public Privilegije()
        {
            this.Korisnik = new HashSet<Korisnik>();
        }
    
        public int idPrivilegije { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public bool add_doktor { get; set; }
        public bool add_pacijent { get; set; }
        public bool del_doktor { get; set; }
        public bool del_pacijent { get; set; }
        public bool modify_doktor { get; set; }
        public bool modify_pacijent { get; set; }
        public bool add_new_privilegije { get; set; }
        public bool pregled_kartona { get; set; }
        public bool modify_kartona { get; set; }
        public bool ažuriranje_opreme { get; set; }
        public bool zakazivanje_termina { get; set; }
    
        public virtual ICollection<Korisnik> Korisnik { get; set; }
    }
}