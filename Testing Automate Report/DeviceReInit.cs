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
    
    public partial class DeviceReInit
    {
        public int DeviceReInitID { get; set; }
        public int TerminalDeviceID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> ReInitTime { get; set; }
        public int Status { get; set; }
    
        public virtual TerminalDevice TerminalDevice { get; set; }
        public virtual User User { get; set; }
    }
}
