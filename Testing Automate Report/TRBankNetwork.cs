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
    
    public partial class TRBankNetwork
    {
        public int ID { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankCode { get; set; }
        public string BranchName { get; set; }
        public string CountryCode { get; set; }
        public bool IsActive { get; set; }
    
        public virtual TRCountry TRCountry { get; set; }
    }
}
