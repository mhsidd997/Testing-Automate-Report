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
    
    public partial class TerminalPowerTool
    {
        public int TerminalID { get; set; }
        public Nullable<bool> IsSImageCommand { get; set; }
        public Nullable<System.DateTime> ScreenImageDateTime { get; set; }
        public byte[] ScreenImage { get; set; }
    
        public virtual Terminal Terminal { get; set; }
    }
}
