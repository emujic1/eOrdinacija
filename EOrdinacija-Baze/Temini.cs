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
    
    public partial class Temini
    {
        public int IdTermina { get; set; }
        public int idUposlenika { get; set; }
        public int idPacijenta { get; set; }
        public int idEventa { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual Pacijenti Pacijenti { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }
    }
}
