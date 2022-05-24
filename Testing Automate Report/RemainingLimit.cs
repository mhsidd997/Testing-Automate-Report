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
    
    public partial class RemainingLimit
    {
        public int RemainingLimitID { get; set; }
        public int AgentID { get; set; }
        public int PaymentLimitID { get; set; }
        public int LimitCycleID { get; set; }
        public int InstitutionID { get; set; }
        public double LimitUsed { get; set; }
        public Nullable<double> LimitRemaining { get; set; }
        public Nullable<double> LockedLimit { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsOffline { get; set; }
        public Nullable<double> LimitAlloted { get; set; }
    
        public virtual Institution Institution { get; set; }
        public virtual LimitCycle LimitCycle { get; set; }
        public virtual PaymentLimit PaymentLimit { get; set; }
        public virtual User User { get; set; }
    }
}