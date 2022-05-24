//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Testing_Automate_Report
{
    using System;
    using System.Collections.Generic;
    
    public partial class PriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceList()
        {
            this.PriceListRates = new HashSet<PriceListRate>();
        }
    
        public int PriceListID { get; set; }
        public int GSMTypeID { get; set; }
        public Nullable<int> PriceListCategoryID { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual GSMType GSMType { get; set; }
        public virtual PriceListCategory PriceListCategory { get; set; }
        public virtual PriceListCategory PriceListCategory1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceListRate> PriceListRates { get; set; }
    }
}