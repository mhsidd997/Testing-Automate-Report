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
    
    public partial class CustomerReview
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerReview()
        {
            this.CustomerReviewItems = new HashSet<CustomerReviewItem>();
        }
    
        public int CustomerReviewID { get; set; }
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerReviewItem> CustomerReviewItems { get; set; }
        public virtual User User { get; set; }
    }
}
