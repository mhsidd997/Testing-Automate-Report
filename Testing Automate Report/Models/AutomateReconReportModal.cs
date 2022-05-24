using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing_Automate_Report.Models
{
    public class AutomateReconReportModal
    {
        public int TransactionLogID { get; set; }
        public DateTime Timestamp { get; set; }
        public Nullable<DateTime> ResponseTime { get; set; }
        public string SourceDateTime { get; set; }
        public double Biller_Due { get; set; }
        public string KioskID { get; set; }
        public string BillerID { get; set; }
        public string ServiceName { get; set; }
        public string ProductID { get; set; }
        public string ConsumerID { get; set; }
        public double? TotalDeposit { get; set; }
        public string NoteBreakDown { get; set; }
        public int? CashCycleID { get; set; }
        public string BillerReference { get; set; }
        public string Fee { get; set; }
        public double? BillerPayment { get; set; }
        public double? FeeEarned { get; set; }
        public double? BreakageEarned { get; set; }
        public double ServiceCharges { get; set; }
        public double? TotalEarning { get; set; }
        public string Status { get; set; }
        public string Repost { get; set; }
        public string UserID { get; set; }
    }
}
