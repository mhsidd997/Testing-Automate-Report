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
    
    public partial class AdvertisementGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdvertisementGroup()
        {
            this.AdvertisementSets = new HashSet<AdvertisementSet>();
            this.TerminalGroups = new HashSet<TerminalGroup>();
            this.TerminalGroups1 = new HashSet<TerminalGroup>();
        }
    
        public int AdvertisementGroupID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public int InstitutionID { get; set; }
        public string Screen { get; set; }
        public int Version { get; set; }
    
        public virtual AdvertisementGroup AdvertisementGroup1 { get; set; }
        public virtual AdvertisementGroup AdvertisementGroup2 { get; set; }
        public virtual Institution Institution { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvertisementSet> AdvertisementSets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TerminalGroup> TerminalGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TerminalGroup> TerminalGroups1 { get; set; }
    }
}
