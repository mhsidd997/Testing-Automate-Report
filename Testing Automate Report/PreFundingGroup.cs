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
    
    public partial class PreFundingGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreFundingGroup()
        {
            this.UserGroups = new HashSet<UserGroup>();
        }
    
        public int PreFundingGroupID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int InstitutionID { get; set; }
        public Nullable<double> MaximumLimit { get; set; }
        public Nullable<double> MinimumLimit { get; set; }
        public Nullable<bool> RefundAllowed { get; set; }
        public Nullable<double> RefundFee { get; set; }
        public Nullable<double> CreditLimit { get; set; }
        public Nullable<bool> PartialDepositAllowed { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual Institution Institution { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
