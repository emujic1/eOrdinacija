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
    
    public partial class Nalaz
    {
        public int idNalaza { get; set; }
        public int IdUposlenika { get; set; }
        public int IdPacijenta { get; set; }
        public string Tip { get; set; }
        public string Vrijednost { get; set; }
        public string Norma { get; set; }
    
        public virtual Pacijenti Pacijenti { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }
    }
}
