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
    
    public partial class Receipt
    {
        public int ReceiptID { get; set; }
        public int InstitutionID { get; set; }
        public int TemplateID { get; set; }
        public Nullable<int> ChildTemplateID { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    
        public virtual Template Template { get; set; }
    }
}
