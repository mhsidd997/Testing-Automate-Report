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
    
    public partial class Contact
    {
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Latlong { get; set; }
        public string Summary { get; set; }
        public bool IsActive { get; set; }
        public int ContactGroupID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> SequenceNumber { get; set; }
        public int InstitutionID { get; set; }
    
        public virtual ContactGroup ContactGroup { get; set; }
        public virtual Institution Institution { get; set; }
    }
}