//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcCodetrace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bouteille
    {
        public string TypeVin { get; set; }
        public string Region { get; set; }
        public string Producteur { get; set; }
        public string Description { get; set; }
        public Nullable<int> RengementId { get; set; }
        public int Id { get; set; }
    
        public virtual Code Code { get; set; }
        public virtual Rengement Rengement { get; set; }
    }
}
