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
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    
    public partial class Korisnik
    {
        public Korisnik()
        {
            this.Pacijenti = new HashSet<Pacijenti>();
            this.Uposlenik = new HashSet<Uposlenik>();
        }

        public int IdKorisnika { get; set; }
        [Required(ErrorMessage = "Unesite ime")]
        [RegularExpression(@"[A-Z]{1}[a-z]*$", ErrorMessage = "Ime nije dobro!")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [RegularExpression(@"[A-Z]{1}[a-z]*$",ErrorMessage="Prezime nije dobro!")]
        [Required(ErrorMessage = "Unesite prezime")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [RegularExpression(@"[A-Z]{1}[a-z]*$", ErrorMessage = "Ime oca nije dobro!")]
        [Required(ErrorMessage = "Unesite ime oca")]
        [Display(Name = "Ime oca")]
        public string Ime_oca { get; set; }
        [Required(ErrorMessage = "Unesite broj lične karte")]
        [Display(Name = "Broj lične karte")]
        public string Broj_licne { get; set; }
        [Display(Name = "Email adresa")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Unesite datum rođenja")]
        [Display(Name = "Datum rođenja")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime? Datum_rodjenja { get; set; }
        [Required(ErrorMessage = "Unesite mjesto rođenja")]
        [RegularExpression(@"([A-Z]{1}[a-z]*$)|([A-Z]{1}[a-z]*\s[A-Z]{1}[a-z]*$)", ErrorMessage = "Mjesto rođenja nije dobro!")]
        [Display(Name = "Mjesto rođenja")]
        public string Mjesto_rodjenja { get; set; }
        [Required(ErrorMessage = "Unesite mjesto prebivališta")]
        [Display(Name = "Mjesto prebivališta")]
        public string Prebivalište { get; set; }
        [Required(ErrorMessage = "Unesite username")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Unesite password")]
        [StringLength(100, ErrorMessage = "Password mora biti najmanje dug {2} karaktera a najviše {1} .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Unesite JMBG")]
        [RegularExpression(@"[0-9]{13}$", ErrorMessage = "Pogrešan JMBG")]
        public string JMBG { get; set; }
        public int idPrivilegije { get; set; }
    
        public virtual Privilegije Privilegije { get; set; }
        public virtual ICollection<Pacijenti> Pacijenti { get; set; }
        public virtual ICollection<Uposlenik> Uposlenik { get; set; }
    }
}
