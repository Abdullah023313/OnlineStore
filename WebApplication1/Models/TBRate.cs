//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBRate
    {
        public System.Guid IdRate { get; set; }
        public Nullable<double> rate { get; set; }
        public Nullable<System.Guid> IdUser { get; set; }
        public Nullable<System.Guid> IdProduct { get; set; }
    
        public virtual TBProduct TBProduct { get; set; }
        public virtual TBUser TBUser { get; set; }
    }
}
