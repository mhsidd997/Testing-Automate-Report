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
    
    public partial class AlertNotification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AlertNotification()
        {
            this.AlertNotificationUsers = new HashSet<AlertNotificationUser>();
        }
    
        public int AlertNotificationID { get; set; }
        public int AlertID { get; set; }
        public Nullable<int> TerminalID { get; set; }
        public Nullable<int> LevelID { get; set; }
        public int EventID { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public bool IsResolved { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual Alert Alert { get; set; }
        public virtual Terminal Terminal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertNotificationUser> AlertNotificationUsers { get; set; }
    }
}