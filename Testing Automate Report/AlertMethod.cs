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
    
    public partial class AlertMethod
    {
        public int AlertMethodID { get; set; }
        public int AlertID { get; set; }
        public string Method { get; set; }
    
        public virtual Alert Alert { get; set; }
    }
}
