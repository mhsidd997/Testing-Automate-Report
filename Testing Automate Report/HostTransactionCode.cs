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
    
    public partial class HostTransactionCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HostTransactionCode()
        {
            this.CommissionTransactionMethods = new HashSet<CommissionTransactionMethod>();
            this.PerformanceCycles = new HashSet<PerformanceCycle>();
        }
    
        public int HostTransactionCodeID { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public int HostID { get; set; }
        public int InstitutionID { get; set; }
        public bool IsActive { get; set; }
        public string OfflineSupport { get; set; }
        public Nullable<int> TransactionCodeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionTransactionMethod> CommissionTransactionMethods { get; set; }
        public virtual Host Host { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual TransactionCode TransactionCode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerformanceCycle> PerformanceCycles { get; set; }
    }
}
