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
    
    public partial class IconLibrary
    {
        public int IconLibraryID { get; set; }
        public string FileName { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public bool IsUserDefined { get; set; }
        public Nullable<int> InstitutionID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual Institution Institution { get; set; }
    }
}
