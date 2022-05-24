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
    
    public partial class TerminalGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TerminalGroup()
        {
            this.AlertTerminalGroups = new HashSet<AlertTerminalGroup>();
            this.FuelStations = new HashSet<FuelStation>();
            this.Stores = new HashSet<Store>();
            this.Terminals = new HashSet<Terminal>();
            this.TerminalGroupConfigs = new HashSet<TerminalGroupConfig>();
            this.UserTerminalGroups = new HashSet<UserTerminalGroup>();
        }
    
        public int TerminalGroupID { get; set; }
        public string Name { get; set; }
        public int InstitutionID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> TerminalTypeID { get; set; }
        public Nullable<int> AdvertisementGroupID { get; set; }
        public Nullable<int> PrimAdGroupID { get; set; }
    
        public virtual AdvertisementGroup AdvertisementGroup { get; set; }
        public virtual AdvertisementGroup AdvertisementGroup1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertTerminalGroup> AlertTerminalGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuelStation> FuelStations { get; set; }
        public virtual Institution Institution { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Store> Stores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Terminal> Terminals { get; set; }
        public virtual TerminalType TerminalType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TerminalGroupConfig> TerminalGroupConfigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTerminalGroup> UserTerminalGroups { get; set; }
    }
}