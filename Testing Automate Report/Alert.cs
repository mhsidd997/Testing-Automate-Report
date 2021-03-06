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
    
    public partial class Alert
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alert()
        {
            this.AlertLevels = new HashSet<AlertLevel>();
            this.AlertMethods = new HashSet<AlertMethod>();
            this.AlertNotifications = new HashSet<AlertNotification>();
            this.AlertTerminalGroups = new HashSet<AlertTerminalGroup>();
            this.DeviceAlertStatus = new HashSet<DeviceAlertStatu>();
            this.TerminalAlertStatus = new HashSet<TerminalAlertStatu>();
        }
    
        public int AlertID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int InstitutionID { get; set; }
        public int TerminalTypeID { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual Institution Institution { get; set; }
        public virtual TerminalType TerminalType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertLevel> AlertLevels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertMethod> AlertMethods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertNotification> AlertNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertTerminalGroup> AlertTerminalGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceAlertStatu> DeviceAlertStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TerminalAlertStatu> TerminalAlertStatus { get; set; }
    }
}
