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
    
    public partial class DeviceAlertStatu
    {
        public int DeviceAlertStatusID { get; set; }
        public int AlertID { get; set; }
        public int DeviceID { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    
        public virtual Alert Alert { get; set; }
        public virtual Device Device { get; set; }
    }
}
