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
    
    public partial class Invoice
    {
        public int InvoiceID { get; set; }
        public int CorporateID { get; set; }
        public int DepartmentID { get; set; }
        public string Month { get; set; }
        public double Amount { get; set; }
        public System.DateTime DueDate { get; set; }
        public int InvoiceNumber { get; set; }
        public string Atachment { get; set; }
        public bool IsPaid { get; set; }
        public int InstitutionID { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
    
        public virtual Corporate Corporate { get; set; }
        public virtual Department Department { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
