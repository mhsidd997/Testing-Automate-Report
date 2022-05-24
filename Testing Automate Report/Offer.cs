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
    
    public partial class Offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offer()
        {
            this.OfferAttributes = new HashSet<OfferAttribute>();
            this.OfferProvinces = new HashSet<OfferProvince>();
            this.OfferStores = new HashSet<OfferStore>();
        }
    
        public int OfferID { get; set; }
        public string Name { get; set; }
        public string DisplayNameEnglish { get; set; }
        public string DisplayNameArabic { get; set; }
        public int OfferTypeID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public bool IsActive { get; set; }
        public int Sequence { get; set; }
        public int TerminalTypeID { get; set; }
        public int MinimumUnits { get; set; }
        public string IDTypes { get; set; }
        public bool IsOfferActive { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string DesNameArabic { get; set; }
        public string DesNameEnglish { get; set; }
        public bool AssignedToPKG { get; set; }
        public bool PerProvince { get; set; }
        public bool IsFree { get; set; }
        public Nullable<int> Price { get; set; }
        public int NumAddtImage { get; set; }
        public int NumAddtAttachment { get; set; }
        public int OfferSerialID { get; set; }
    
        public virtual OfferType OfferType { get; set; }
        public virtual TerminalType TerminalType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferAttribute> OfferAttributes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferProvince> OfferProvinces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfferStore> OfferStores { get; set; }
    }
}
