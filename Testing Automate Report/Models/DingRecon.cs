    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing_Automate_Report.Models
{
    public class DingRecon
    {
        public string Date { get; set; }
        public string TransferRef { get; set; }
        public string BalanceBefore { get; set; }
        public string BalanceAfter { get; set; }
        public string RecieveAmt { get; set; }
        public string SalesPrice { get; set; }
        public string CostPrice { get; set; }
        public string CommissionAmt { get; set; }
        public int TransactionLogID { get; set; }
        public string TransactionID { get; set; }
        public string Status { get; set; }
        public string country { get; set; }
        public string Operator { get; set; }
        public string Agent { get; set; }
        public string user { get; set; }
        public string PorductSKUcode { get; set; }
    }
}