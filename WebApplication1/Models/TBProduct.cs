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
    
    public partial class TBProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBProduct()
        {
            this.TBCarts = new HashSet<TBCart>();
            this.TBColors = new HashSet<TBColor>();
            this.TBDetails = new HashSet<TBDetail>();
            this.TBImages = new HashSet<TBImage>();
            this.TBRates = new HashSet<TBRate>();
        }
    
        public System.Guid IdProduct { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<double> purchasingPrice { get; set; }
        public string name { get; set; }
        public Nullable<double> sellingPrice { get; set; }
        public string details { get; set; }
        public string description { get; set; }
        public string company { get; set; }
        public Nullable<int> rate { get; set; }
        public Nullable<System.Guid> IdType { get; set; }
        public Nullable<bool> show { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBCart> TBCarts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBColor> TBColors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBDetail> TBDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBImage> TBImages { get; set; }
        public virtual TBProductType TBProductType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBRate> TBRates { get; set; }
    }
}
