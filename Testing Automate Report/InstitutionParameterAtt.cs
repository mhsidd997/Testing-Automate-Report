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
    
    public partial class InstitutionParameterAtt
    {
        public int InstitutionParameterAttID { get; set; }
        public int TransactionAttributeID { get; set; }
        public Nullable<int> AppMenuID { get; set; }
        public string Value { get; set; }
        public bool IsDraft { get; set; }
    
        public virtual AppMenu AppMenu { get; set; }
        public virtual TransactionAttribute TransactionAttribute { get; set; }
    }
}
