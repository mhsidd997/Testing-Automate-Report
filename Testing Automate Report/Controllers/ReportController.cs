using LumenWorks.Framework.IO.Csv;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing_Automate_Report.Models;

namespace Testing_Automate_Report.Controllers
{
    public class ReportController : Controller
    {
        PAYdevEntities db = new PAYdevEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        int row1;
        // GET: Report
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files)
        {
            if (files == null)
            { return View(); }
            string path = Server.MapPath("~/App_Data/DingFile");
            string filename = Path.GetFileName(files.FileName);
            string fullpath = Path.Combine(path, filename);
            files.SaveAs(fullpath);



            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(fullpath)), true))
            {
                csvTable.Load(csvReader);
            }

            row1 = csvTable.Rows.Count;
            List<DingRecon> DingDataList = new List<DingRecon>();
            for (int i = 0; i < row1; i++)
            {
                int DistribRef = csvTable.Rows[i][9].ToString().Trim() == "N/A" ? 0000 : Convert.ToInt32(csvTable.Rows[i][9]);
                DingDataList.Add(new DingRecon
                {
                    Date = csvTable.Rows[i][0].ToString(),
                    TransactionID = csvTable.Rows[i][1].ToString(),
                    BalanceBefore = csvTable.Rows[i][2].ToString(),
                    BalanceAfter = csvTable.Rows[i][3].ToString(),
                    RecieveAmt = csvTable.Rows[i][4].ToString(),
                    SalesPrice = csvTable.Rows[i][5].ToString(),
                    CostPrice = csvTable.Rows[i][6].ToString(),
                    CommissionAmt = csvTable.Rows[i][7].ToString(),
                    TransferRef = csvTable.Rows[i][8].ToString(),//TransferRef Change
                    TransactionLogID = DistribRef, //DistribRef
                    Status = csvTable.Rows[i][10].ToString(),
                    country = csvTable.Rows[i][11].ToString(),
                    Operator = csvTable.Rows[i][12].ToString(),
                    Agent = csvTable.Rows[i][13].ToString(),
                    user = csvTable.Rows[i][14].ToString(),
                    PorductSKUcode = csvTable.Rows[i][15].ToString(),

                });
            }

            string[] month = csvTable.Rows[0][0].ToString().Split(new char[] { '-', '/' });
            var updatedMonth = month[1].Length == 1 ? '0' + month[1].ToString() : month[1].ToString();
            var days = DateTime.DaysInMonth(Convert.ToInt32(month[1]), Convert.ToInt32(updatedMonth));


            int terminalID = 0;
            DateTime? from = null;
            DateTime? to = null;
            string startdate = "01/" + updatedMonth + "/" + DateTime.Now.Year + " 00:00";
            string enddate = days + "/" + updatedMonth + "/" + DateTime.Now.Year + " 23:59";

            if (!string.IsNullOrEmpty(startdate))
            {
                from = DateTime.ParseExact(startdate, "dd/MM/yyyy H:mm", null);
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                to = DateTime.ParseExact(enddate, "dd/MM/yyyy H:mm", null);
                TimeSpan ts = new TimeSpan(to.Value.Hour, to.Value.Minute, 59);
                to = to.Value.Date + ts;
            }

            DateTime? cashCollectionStartTime = null;
            DateTime? cashCollectionEndTime = null;
            int? cashCycle = null;
            int status = -1;
            List<int> kioskID = new List<int>();
            List<int> billerID = new List<int>();
            List<int> cashCollectionID = new List<int>();
            string handle = null;
            bool flag = false;

            //TGPay Report
            #region
            List<AutomateReconReportModal> reconciliationReport = (db.TransactionLogs
                                           .Join(
                                             db.AppMenuTemplates,
                                             tlog => tlog.AppMenuID,
                                             amt => amt.AppMenuID,
                                             (tlog, amt) => new
                                             {
                                                 tlog,
                                                 amt.TemplateID
                                             })
                                            .Join(db.RequestTypes,
                                                 tl => tl.TemplateID,
                                                 rt => rt.TemplateID,
                                                 (tl, rt) => new
                                                 {
                                                     tl.tlog,
                                                     tl.TemplateID,
                                                     rt.IsFinancial,
                                                     RequestTypeCode = rt.Code
                                                 })
                                            .Join(db.TransactionCodes,
                                                  tl => tl.tlog.ServiceID,
                                                  tcode => tcode.Code,
                                                  (tl, tcode) => new
                                                  {
                                                      tl.tlog,
                                                      tl.IsFinancial,
                                                      tl.TemplateID,
                                                      ServiceName = tcode.ShortName,
                                                      tcode.Code,
                                                      tcode.TransactionCodeID,
                                                      tl.RequestTypeCode

                                                  })
                                            .GroupJoin(db.TerminalCashCollections,
                                                  tl => tl.tlog.TerminalCashCollectionID,
                                                  cc => cc.TerminalCashCollectionID,
                                                  (tl, cc) => new
                                                  {
                                                      tl,
                                                      cc
                                                  })
                                            .SelectMany(tcc => tcc.cc.DefaultIfEmpty(),
                                                  (t, tcc) => new
                                                  {
                                                      t.tl,
                                                      TerminalCashCycle = tcc

                                                  })
                                                .GroupJoin(
                                                   db.HostTransactionCodes,
                                                   tcode => tcode.tl.TransactionCodeID,
                                                   hcode => hcode.TransactionCodeID,
                                                   (tcode, hcode) => new
                                                   {
                                                       tcode,
                                                       hcode

                                                   })
                                           .SelectMany(h => h.hcode.DefaultIfEmpty(),
                                            (t, h) => new
                                            {
                                                t.tcode.tl.tlog,
                                                t.tcode.tl.IsFinancial,
                                                t.tcode.tl.TemplateID,
                                                t.tcode.tl.Code,
                                                t.tcode.tl.ServiceName,
                                                t.tcode.tl.RequestTypeCode,
                                                t.tcode.TerminalCashCycle,
                                                h.Host.HostID,
                                                HostName = h.Host.Name ?? ""
                                            })
                                          .Where(tl => tl.tlog.Terminal.TerminalTypeID == 2
                                                 && tl.IsFinancial
                                                 && tl.tlog.ServiceRequestType == tl.RequestTypeCode
                                                 && ((kioskID.Count == 0) || (kioskID.Count > 0 && kioskID.Contains(tl.tlog.TerminalID.Value)))
                                                 && ((billerID.Count == 0) || (billerID.Count > 0 && billerID.Contains(tl.HostID)))
                                                 && ((status == -1) || (tl.tlog.ResponseCodeCategoryID != null && tl.tlog.ResponseCodeCategoryID == status))
                                                 && ((cashCycle == null) || (cashCycle == 0 && !tl.TerminalCashCycle.IsCollected) || (cashCollectionStartTime != null ?
                                                 (tl.tlog.Timestamp >= cashCollectionStartTime && tl.tlog.Timestamp <= cashCollectionEndTime)
                                                 : tl.tlog.Timestamp <= cashCollectionEndTime))
                                                 && ((from == null && to == null) || (from != null && tl.tlog.Timestamp >= from.Value)
                                                                                    && (to != null && tl.tlog.Timestamp <= to.Value)))

                                                 .Select(tl => new AutomateReconReportModal
                                                 {
                                                     TransactionLogID = tl.tlog.TransactionLogID,
                                                     Timestamp = tl.tlog.Timestamp,
                                                     ResponseTime = tl.tlog.ResponseTime,
                                                     KioskID = tl.tlog.Terminal.TerminalNumber,
                                                     BillerID = tl.HostName,
                                                     ServiceName = tl.ServiceName,
                                                     ProductID = tl.tlog.ServiceProductID,
                                                     ConsumerID = tl.tlog.ServiceConsumerNumber,
                                                     TotalDeposit = tl.tlog.PaymentAmount ?? 0,
                                                     NoteBreakDown = tl.tlog.NoteBreakdown,
                                                     CashCycleID = tl.tlog.TerminalCashCollectionID ?? 0,
                                                     BillerReference = tl.tlog.RequestAdditionalInfo5 ?? "N/A",
                                                     Fee = tl.tlog.RequestAdditionalInfo3 ?? "0",
                                                     BillerPayment = tl.tlog.TransactionAmount ?? 0,
                                                     FeeEarned = tl.tlog.TransactionFee != null ? Math.Round(tl.tlog.TransactionFee.Value, 3) : 0,
                                                     Biller_Due = (tl.tlog.TransactionAmount ?? 0) - (tl.tlog.TransactionFee != null ? Math.Round(tl.tlog.TransactionFee.Value, 3) : 0),
                                                     BreakageEarned = tl.tlog.BreakageEarned ?? 0,
                                                     ServiceCharges = 0,
                                                     TotalEarning = (tl.tlog.TotalEarning) != null ? Math.Round(tl.tlog.TotalEarning.Value, 3) : 0,
                                                     Status = tl.tlog.ResponseCodeCategory.Name,
                                                     Repost = db.RepostDetails.Where(x => x.Status == "Approved"
                                                               && x.DisputedTransaction.TransactionLogID == tl.tlog.TransactionLogID)
                                                              .Count() > 0 ? "Y" : "N",
                                                     UserID = db.RepostDetails.FirstOrDefault(x => x.Status == "Approved"
                                                             && x.DisputedTransaction.TransactionLogID == tl.tlog.TransactionLogID).User.Name ?? "N/A"
                                                 }).Distinct().AsQueryable().OrderByDescending(x => x.TransactionLogID).ToList()
                                   );
            #endregion
            if (reconciliationReport.Count() > DingDataList.Count())
                flag = true;

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Reconciliation Report");
            ExcelWorksheet Sheet1 = Ep.Workbook.Worksheets.Add("Ding Report");
            ExcelWorksheet Sheet2 = Ep.Workbook.Worksheets.Add("Stats");

            int row = 2;
            string currentformula = "";

            #region * Ding Column details
            Sheet1.Cells["A1"].Value = "Date";
            Sheet1.Cells["B1"].Value = "TransactionID";
            Sheet1.Cells["C1"].Value = "Balance Before";
            Sheet1.Cells["D1"].Value = "Balance After";
            Sheet1.Cells["E1"].Value = "Receive Amount";
            Sheet1.Cells["F1"].Value = "Sales Price";
            Sheet1.Cells["G1"].Value = "Cost Price";
            Sheet1.Cells["H1"].Value = "Commission Amount";
            Sheet1.Cells["I1"].Value = "Transfer Ref";
            Sheet1.Cells["J1"].Value = "Distributor Ref";
            Sheet1.Cells["K1"].Value = "Status";
            Sheet1.Cells["L1"].Value = "Country";
            Sheet1.Cells["M1"].Value = "Operator";
            Sheet1.Cells["N1"].Value = "Agent";
            Sheet1.Cells["O1"].Value = "User";
            Sheet1.Cells["P1"].Value = "Product SKU code";
            #endregion
            foreach (var ding in DingDataList)
            {
                string[] receiverAmount = ding.RecieveAmt.Split(' ');
                AutomateReconReportModal recon = reconciliationReport.FirstOrDefault(x => x.BillerID == "Ding Host" && x.TransactionLogID == ding.TransactionLogID && x.Status != ding.Status);
                if (recon != null)
                {
                    if (ding.Status == "Failure")
                        recon.Status = "Declined";
                    else
                        recon.Status = "Approved";
                }
                Sheet1.Cells[string.Format("A{0}", row)].Value = ding.Date.ToString(); //
                Sheet1.Cells[string.Format("B{0}", row)].Value = ding.TransactionID.ToString();
                Sheet1.Cells[string.Format("C{0}", row)].Value = ding.BalanceBefore;
                Sheet1.Cells[string.Format("D{0}", row)].Value = ding.BalanceAfter.ToString() ?? "N/A"; //a.TransactionCurrency;
                Sheet1.Cells[string.Format("E{0}", row)].Value = Convert.ToDouble(receiverAmount[0]); //
                Sheet1.Cells[string.Format("F{0}", row)].Value = Convert.ToDouble(ding.SalesPrice);
                Sheet1.Cells[string.Format("G{0}", row)].Value = Convert.ToDouble(ding.CostPrice);
                Sheet1.Cells[string.Format("H{0}", row)].Value = Convert.ToDouble(ding.CommissionAmt);
                Sheet1.Cells[string.Format("I{0}", row)].Value = ding.TransferRef;
                Sheet1.Cells[string.Format("J{0}", row)].Value = ding.TransactionLogID.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("K{0}", row)].Value = ding.Status;
                Sheet1.Cells[string.Format("L{0}", row)].Value = ding.country.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("M{0}", row)].Value = ding.Operator;
                Sheet1.Cells[string.Format("N{0}", row)].Value = ding.Agent;
                Sheet1.Cells[string.Format("O{0}", row)].Value = ding.user;
                Sheet1.Cells[string.Format("p{0}", row)].Value = ding.PorductSKUcode;

                row++;
            }
            Sheet1.Cells["A:AZ"].AutoFitColumns();


            #region  * TGPay Column details
            Sheet.Cells["A1"].Value = "Pd Txn ID";
            Sheet.Cells["B1"].Value = "Source DateTime";
            Sheet.Cells["C1"].Value = "Response Time";
            Sheet.Cells["D1"].Value = "Kiosk ID";
            Sheet.Cells["E1"].Value = "Biller ID";
            Sheet.Cells["F1"].Value = "Service";
            Sheet.Cells["G1"].Value = "Product";
            Sheet.Cells["H1"].Value = "Consumer ID";
            Sheet.Cells["I1"].Value = "Total Deposit";
            Sheet.Cells["J1"].Value = "Note Breakdown";
            Sheet.Cells["K1"].Value = "Cash Cycle";
            Sheet.Cells["L1"].Value = "Biller Reference";
            Sheet.Cells["M1"].Value = "Fee";
            Sheet.Cells["N1"].Value = "Biller Payment";
            Sheet.Cells["O1"].Value = "Fee Earned";
            Sheet.Cells["P1"].Value = "Breakage Earned";
            Sheet.Cells["Q1"].Value = "Service Charges";
            Sheet.Cells["R1"].Value = "Total Earning";
            Sheet.Cells["S1"].Value = "Status";
            Sheet.Cells["T1"].Value = "Repost";
            Sheet.Cells["U1"].Value = "User";
            Sheet.Cells["V1"].Value = "Biller Due";
            #endregion
            row = 2;
            currentformula = "";
            foreach (var a in reconciliationReport)
            {
                currentformula = "=(N" + row + "-O" + row + ")";
                Sheet.Cells[string.Format("A{0}", row)].Value = a.TransactionLogID.ToString(); //
                Sheet.Cells[string.Format("B{0}", row)].Value = a.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"); //a.SourceTransactionID;
                Sheet.Cells[string.Format("C{0}", row)].Value = a.ResponseTime != null ? a.ResponseTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                Sheet.Cells[string.Format("D{0}", row)].Value = a.KioskID.ToString() ?? "N/A"; //a.TransactionCurrency;
                Sheet.Cells[string.Format("E{0}", row)].Value = a.BillerID.ToString() ?? "N/A"; //
                Sheet.Cells[string.Format("F{0}", row)].Value = a.ServiceName ?? "N/A";
                Sheet.Cells[string.Format("G{0}", row)].Value = a.ProductID ?? "N/A";
                Sheet.Cells[string.Format("H{0}", row)].Value = a.ConsumerID ?? "N/A";
                Sheet.Cells[string.Format("I{0}", row)].Value = Convert.ToDouble(a.TotalDeposit.ToString() ?? "0");
                Sheet.Cells[string.Format("J{0}", row)].Value = a.NoteBreakDown ?? "N/A";
                Sheet.Cells[string.Format("K{0}", row)].Value = a.CashCycleID.ToString() ?? "N/A";
                Sheet.Cells[string.Format("L{0}", row)].Value = a.BillerReference.ToString() ?? "N/A";
                Sheet.Cells[string.Format("M{0}", row)].Value = a.Fee.ToString() ?? "0%";
                Sheet.Cells[string.Format("N{0}", row)].Value = Convert.ToDouble(a.BillerPayment.ToString() ?? "0");
                Sheet.Cells[string.Format("O{0}", row)].Value = Convert.ToDouble(a.FeeEarned.ToString() ?? "0");
                Sheet.Cells[string.Format("P{0}", row)].Value = Convert.ToDouble(a.BreakageEarned.ToString() ?? "0");
                Sheet.Cells[string.Format("Q{0}", row)].Value = Convert.ToInt32(a.ServiceCharges.ToString() ?? "0");
                Sheet.Cells[string.Format("R{0}", row)].Value = Convert.ToDouble(a.TotalEarning.ToString() ?? "0");
                Sheet.Cells[string.Format("S{0}", row)].Value = a.Status.ToString() ?? "N/A";
                Sheet.Cells[string.Format("T{0}", row)].Value = a.Repost.ToString() ?? "N/A";
                Sheet.Cells[string.Format("U{0}", row)].Value = a.UserID.ToString() ?? "N/A";
                Sheet.Cells[string.Format("V{0}", row)].Value = Convert.ToDouble(a.Biller_Due.ToString() ?? "0");

                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();

            //Add logo
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("~/Content/Image/Logo.png")))
            {
                var excelImage = Sheet2.Drawings.AddPicture("My Logo", image);

                //add the image to row 2, column B
                excelImage.SetPosition(1, 0, 1, 0);
                excelImage.SetSize(150, 75);
            }

            //define the data range on the source sheet
            var dataRange = Sheet.Cells[Sheet.Dimension.Address];
            var dataRange1 = Sheet1.Cells[Sheet1.Dimension.Address];

            //create the pivot table
            var pivotTable = Sheet2.PivotTables.Add(Sheet2.Cells["B8"], dataRange, "PivotTable");
            var pivotTable1 = Sheet2.PivotTables.Add(Sheet2.Cells["B33"], dataRange1, "PivotTable1");

            //label field for TGpay report
            pivotTable.RowFields.Add(pivotTable.Fields["Biller ID"]);
            pivotTable.DataOnRows = false;
            pivotTable.RowFields.Add(pivotTable.Fields["Status"]);
            pivotTable.DataOnRows = false;
            //data field
            var field = pivotTable.DataFields.Add(pivotTable.Fields["Kiosk ID"]);
            field.Name = "Count of Kiosk ID";
            field.Function = DataFieldFunctions.Count;
            field = pivotTable.DataFields.Add(pivotTable.Fields["Total Deposit"]);
            field.Name = "Sum of Total Deposit";
            field.Function = DataFieldFunctions.Sum;
            field.Format = "0.00";
            field = pivotTable.DataFields.Add(pivotTable.Fields["Biller Payment"]);
            field.Name = "Sum of Biller Payment";
            field.Function = DataFieldFunctions.Sum;
            field.Format = "0.00";
            field = pivotTable.DataFields.Add(pivotTable.Fields["Fee Earned"]);
            field.Name = "Sum of Fee Earned";
            field.Function = DataFieldFunctions.Sum;
            field.Format = "0.00";
            field = pivotTable.DataFields.Add(pivotTable.Fields["Breakage Earned"]);
            field.Name = "Sum of Breakage Earned";
            field.Function = DataFieldFunctions.Sum;
            field.Format = "0.00";
            field = pivotTable.DataFields.Add(pivotTable.Fields["Total Earning"]);
            field.Name = "Sum of Total Earning";
            field.Function = DataFieldFunctions.Sum;
            field.Format = "0.00";
            field = pivotTable.DataFields.Add(pivotTable.Fields["Biller Due"]);
            field.Name = "Sum of Biller Due";
            field.Function = DataFieldFunctions.Sum;
            field.Format = "0.00";

            //label field for Host report
            pivotTable1.RowFields.Add(pivotTable1.Fields["Status"]);
            pivotTable1.DataOnRows = false;
            pivotTable1.RowFields.Add(pivotTable1.Fields["User"]);
            pivotTable1.DataOnRows = false;
            //data field
            var field1 = pivotTable1.DataFields.Add(pivotTable1.Fields["TransactionID"]);
            field1.Name = "Count of TransactionID";
            field1.Function = DataFieldFunctions.Count;

            field1 = pivotTable1.DataFields.Add(pivotTable1.Fields["Receive Amount"]);
            field1.Name = "Sum of Receive Amount";
            field1.Function = DataFieldFunctions.Sum;

            field1.Format = "0.00";
            field1 = pivotTable1.DataFields.Add(pivotTable1.Fields["Sales Price"]);
            field1.Name = "Sum of Sales Price";
            field1.Function = DataFieldFunctions.Sum;
            field1.Format = "0.00";
            field1 = pivotTable1.DataFields.Add(pivotTable1.Fields["Cost Price"]);
            field1.Name = "Sum of Cost Price";
            field1.Function = DataFieldFunctions.Sum;
            field1.Format = "0.00";
            field1 = pivotTable1.DataFields.Add(pivotTable1.Fields["Commission Amount"]);
            field1.Name = "Commission Amount";
            field1.Function = DataFieldFunctions.Sum;
            field1.Format = "0.00";
            //----------------------------------------//
            //label field for Final PivotTable

            using (ExcelRange Rng = Sheet2.Cells["B51:G57"])
            {
                Rng.Merge = false;
                Rng.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                Rng.Style.Border.Top.Color.SetColor(Color.Black);
                Rng.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                Rng.Style.Border.Left.Color.SetColor(Color.Black);
                Rng.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                Rng.Style.Border.Right.Color.SetColor(Color.Black);
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                Rng.Style.Border.Bottom.Color.SetColor(Color.Black);

                Sheet2.Cells["B51"].Value = " ";
                Sheet2.Cells["B52"].Value = "Ding";

                Sheet2.Cells["B53"].Value = "Online Txns";
                Sheet2.Cells["C53"].Formula = @"=GETPIVOTDATA(""Count of TransactionID"",Stats!$B$33,""Status"",""Success"",""User"",""API User"")";
                Sheet2.Cells["B54"].Value = "Manual Posting";
                Sheet2.Cells["C54"].Formula = @"=GETPIVOTDATA(""Count of TransactionID"",Stats!$B$33,""Status"",""Success"",""User"",""mohammed.arafath@transguardgroup.com"")";
                Sheet2.Cells["C52"].Formula = @"=SUM(C53,C54)";
                Sheet2.Cells["B55"].Value = "Etisalat";
                Sheet2.Cells["C55"].Formula = @"=GETPIVOTDATA(""Count of Kiosk ID"",Stats!$B$8,""Biller ID"",""Etisalat"",""Status"",""Approved"")";
                Sheet2.Cells["B56"].Value = "Paykii";
                Sheet2.Cells["C56"].Formula = @"=GETPIVOTDATA(""Count of Kiosk ID"",Stats!$B$8,""Biller ID"",""Paykii"",""Status"",""Approved"")";
                Sheet2.Cells["B57"].Value = "TOTAL";
                Sheet2.Cells["C57"].Formula = @"=SUM(C52,C55,C56)";
                //Fee Earned
                Sheet2.Cells["D53"].Formula = @"=GETPIVOTDATA(""Sum of Fee Earned"",Stats!$B$8, ""Biller ID"", ""Ding Host"", ""Status"", ""Approved"")";
                Sheet2.Cells["D54"].Formula = @"=GETPIVOTDATA(""Commission Amount"",Stats!$B$33, ""Status"",""Success"",""User"",""mohammed.arafath@transguardgroup.com"")";
                Sheet2.Cells["D52"].Formula = @"=SUM(D53,D54)";
                Sheet2.Cells["D55"].Formula = @"=GETPIVOTDATA(""Sum of Total Earning"",Stats!$B$8, ""Biller ID"", ""Etisalat"", ""Status"", ""Approved"")";
                Sheet2.Cells["D56"].Formula = @"=GETPIVOTDATA(""Sum of Fee Earned"",Stats!$B$8, ""Biller ID"", ""Paykii"", ""Status"", ""Approved"")";
                Sheet2.Cells["D57"].Formula = @"=SUM(D52,D55,D56)";
                //Breakage Earned
                Sheet2.Cells["E53"].Formula = @"=GETPIVOTDATA(""Sum of Breakage Earned"",Stats!$B$8, ""Biller ID"", ""Ding Host"", ""Status"", ""Approved"")";
                Sheet2.Cells["E52"].Formula = @"=SUM(E53)";
                Sheet2.Cells["E56"].Formula = @"=GETPIVOTDATA(""Sum of Breakage Earned"",Stats!$B$8, ""Biller ID"", ""Paykii"", ""Status"", ""Approved"")";
                Sheet2.Cells["E57"].Formula = @"=SUM(E56,E52)";
                //Total
                Sheet2.Cells["F53"].Formula = @"=SUM(D53,E53)";
                Sheet2.Cells["F54"].Formula = @"=SUM(D54,E54)";
                Sheet2.Cells["F52"].Formula = @"=SUM(F53,F54)";
                Sheet2.Cells["F55"].Formula = @"=SUM(D55,E55)";
                Sheet2.Cells["F56"].Formula = @"=SUM(D56,E56)";
                Sheet2.Cells["F57"].Formula = @"=SUM(F52,F55,F56)";
                //Biller Due
                Sheet2.Cells["G53"].Formula = @"=GETPIVOTDATA(""Sum of Biller Due"",Stats!$B$8, ""Biller ID"", ""Ding Host"", ""Status"", ""Approved"")";
                Sheet2.Cells["G54"].Formula = @"=GETPIVOTDATA(""Sum of Cost Price"",Stats!$B$33, ""Status"",""Success"",""User"",""mohammed.arafath@transguardgroup.com"")";
                Sheet2.Cells["G52"].Formula = @"=SUM(G53,G54)";
                Sheet2.Cells["G55"].Formula = @"=GETPIVOTDATA(""Sum of Biller Due"",Stats!$B$8, ""Biller ID"", ""Etisalat"", ""Status"", ""Approved"")";
                Sheet2.Cells["G56"].Formula = @"=GETPIVOTDATA(""Sum of Biller Due"",Stats!$B$8, ""Biller ID"", ""Paykii"", ""Status"", ""Approved"")";
                Sheet2.Cells["G57"].Formula = @"=SUM(G52,G55,G56)";


                Sheet2.Cells["C51"].Value = "Count";
                Sheet2.Cells["D51"].Value = "Fee Earned";
                Sheet2.Cells["E51"].Value = "Breakage Earned";
                Sheet2.Cells["F51"].Value = "Total";
                Sheet2.Cells["G51"].Value = "Biller Due";

                Sheet2.Cells["C51:G51"].Style.Font.Size = 14;
                Sheet2.Cells["C51:G51"].Style.Font.Name = "Calibri";
                Sheet2.Cells["C51:G51"].Style.Font.Bold = true;
                Sheet2.Cells["C51:G51"].Style.Font.Color.SetColor(Color.Black);
                Sheet2.Cells["B52:B57"].Style.Font.Size = 12;
                Sheet2.Cells["B52:B57"].Style.Font.Name = "Calibri";
                Sheet2.Cells["B52:B57"].Style.Font.Bold = true;
                Sheet2.Cells["B52:B57"].Style.Font.Color.SetColor(Color.Black);
                Sheet2.Cells["C52:G52"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["C52:G52"].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                Sheet2.Cells["C57:G57"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["C57:G57"].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                Sheet2.Cells["C51:G51"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["C51:G51"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                Sheet2.Cells["F53:F56"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["F53:F56"].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                Sheet2.Cells["C53:E56"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["C53:E56"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                Sheet2.Cells["G53:G56"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                Sheet2.Cells["G53:G56"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                //Heading 1
                ExcelRange rg = Sheet2.Cells["B7"];
                rg.IsRichText = true;
                //ExcelRichText uses "using OfficeOpenXml.Style;"
                ExcelRichText text1 = rg.RichText.Add("TG Pay Details");
                text1.Bold = true;
                text1.FontName = "Calibri";
                text1.Size = 18;
                text1.Color = System.Drawing.Color.Black;
                //Heading 2
                ExcelRange h2 = Sheet2.Cells["B31"];
                h2.IsRichText = true;
                //ExcelRichText uses "using OfficeOpenXml.Style;"
                ExcelRichText text2 = h2.RichText.Add("Ding Details");
                text2.Bold = true;
                text2.FontName = "Calibri";
                text2.Size = 18;
                text2.Color = System.Drawing.Color.Black;
                //Heading 2
                ExcelRange h3 = Sheet2.Cells["B50"];
                h3.IsRichText = true;
                //ExcelRichText uses "using OfficeOpenXml.Style;"
                ExcelRichText text3 = h3.RichText.Add("Total Earning");
                text3.Bold = true;
                text3.FontName = "Calibri";
                text3.Size = 18;
                text3.Color = System.Drawing.Color.Black;
                //Footer
                ExcelRange g = Sheet2.Cells["D60"];
                g.IsRichText = true;
                //ExcelRichText uses "using OfficeOpenXml.Style;"
                ExcelRichText Footer = g.RichText.Add("© 2022 Encore-Pay");
                Footer.Bold = true;
                //Footer.Italic = true;
                Footer.FontName = "Calibri";
                Footer.Size = 11;
                Footer.Color = System.Drawing.Color.Black;
            }
            handle = Guid.NewGuid().ToString();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Ep.SaveAs(memoryStream);
                memoryStream.Position = 0;
                TempData[handle] = memoryStream.ToArray();
            }
            return RedirectToAction("DownloadReconciliationReport", "Report", new { fileGuid = handle, fileName = filename });

        }
        [HttpGet]
        public ActionResult DownloadReconciliationReport(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }
    }
}