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
    
    public partial class InstitutionCurrency
    {
        public int InstitutionCurrencyID { get; set; }
        public int InstitutionID { get; set; }
        public int CurrencyID { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
