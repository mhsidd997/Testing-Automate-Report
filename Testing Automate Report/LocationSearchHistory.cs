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
    
    public partial class LocationSearchHistory
    {
        public int LocationSearchID { get; set; }
        public int CustomerID { get; set; }
        public int LocationID { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual CustomerLocationSearch CustomerLocationSearch { get; set; }
    }
}
