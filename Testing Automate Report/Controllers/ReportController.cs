using LumenWorks.Framework.IO.Csv;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
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



            var csvTable = new System.Data.DataTable();
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
            var year = DateTime.Now.ToString("yyyy");

            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(updatedMonth));
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
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Stats");
            ExcelWorksheet Sheet1 = Ep.Workbook.Worksheets.Add("TGPay Report");
            ExcelWorksheet Sheet2 = Ep.Workbook.Worksheets.Add("Ding Report");

            int row = 2;
            string currentformula = "";

            #region * Ding Column details
            Sheet2.Cells["A1"].Value = "Date";
            Sheet2.Cells["B1"].Value = "TransactionID";
            Sheet2.Cells["C1"].Value = "Balance Before";
            Sheet2.Cells["D1"].Value = "Balance After";
            Sheet2.Cells["E1"].Value = "Receive Amount";
            Sheet2.Cells["F1"].Value = "Sales Price";
            Sheet2.Cells["G1"].Value = "Cost Price";
            Sheet2.Cells["H1"].Value = "Commission Amount";
            Sheet2.Cells["I1"].Value = "Transfer Ref";
            Sheet2.Cells["J1"].Value = "Distributor Ref";
            Sheet2.Cells["K1"].Value = "Status";
            Sheet2.Cells["L1"].Value = "Country";
            Sheet2.Cells["M1"].Value = "Operator";
            Sheet2.Cells["N1"].Value = "Agent";
            Sheet2.Cells["O1"].Value = "User";
            Sheet2.Cells["P1"].Value = "Product SKU code";
            #endregion
            foreach (var ding in DingDataList)
            {
                string[] receiverAmount = ding.RecieveAmt.Split(' ');
                AutomateReconReportModal recon = reconciliationReport.FirstOrDefault(x => x.BillerID == "MHZ-MA" && x.TransactionLogID == ding.TransactionLogID && x.Status != ding.Status);
                if (recon != null)
                {
                    if (ding.Status == "Failure")
                        recon.Status = "Declined";
                    else
                        recon.Status = "Approved";
                }
                if (ding.user == "API User")
                    ding.user = "TGPay";
                else
                    ding.user = "Ding Portal";
                Sheet2.Cells[string.Format("A{0}", row)].Value = ding.Date.ToString(); //
                Sheet2.Cells[string.Format("B{0}", row)].Value = ding.TransactionID.ToString();
                Sheet2.Cells[string.Format("C{0}", row)].Value = ding.BalanceBefore;
                Sheet2.Cells[string.Format("D{0}", row)].Value = ding.BalanceAfter.ToString() ?? "N/A"; //a.TransactionCurrency;
                Sheet2.Cells[string.Format("E{0}", row)].Value = Convert.ToDouble(receiverAmount[0]); //
                Sheet2.Cells[string.Format("F{0}", row)].Value = Convert.ToDouble(ding.SalesPrice);
                Sheet2.Cells[string.Format("G{0}", row)].Value = Convert.ToDouble(ding.CostPrice);
                Sheet2.Cells[string.Format("H{0}", row)].Value = Convert.ToDouble(ding.CommissionAmt);
                Sheet2.Cells[string.Format("I{0}", row)].Value = ding.TransferRef;
                Sheet2.Cells[string.Format("J{0}", row)].Value = ding.TransactionLogID.ToString() ?? "N/A";
                Sheet2.Cells[string.Format("K{0}", row)].Value = ding.Status;
                Sheet2.Cells[string.Format("L{0}", row)].Value = ding.country.ToString() ?? "N/A";
                Sheet2.Cells[string.Format("M{0}", row)].Value = ding.Operator;
                Sheet2.Cells[string.Format("N{0}", row)].Value = ding.Agent;
                Sheet2.Cells[string.Format("O{0}", row)].Value = ding.user;
                Sheet2.Cells[string.Format("p{0}", row)].Value = ding.PorductSKUcode;

                row++;
            }
            Sheet2.Cells["A:AZ"].AutoFitColumns();


            #region  * TGPay Column details
            Sheet1.Cells["A1"].Value = "Pd Txn ID";
            Sheet1.Cells["B1"].Value = "Source DateTime";
            Sheet1.Cells["C1"].Value = "Response Time";
            Sheet1.Cells["D1"].Value = "Kiosk ID";
            Sheet1.Cells["E1"].Value = "Biller ID";
            Sheet1.Cells["F1"].Value = "Service";
            Sheet1.Cells["G1"].Value = "Product";
            Sheet1.Cells["H1"].Value = "Consumer ID";
            Sheet1.Cells["I1"].Value = "Total Deposit";
            Sheet1.Cells["J1"].Value = "Note Breakdown";
            Sheet1.Cells["K1"].Value = "Cash Cycle";
            Sheet1.Cells["L1"].Value = "Biller Reference";
            Sheet1.Cells["M1"].Value = "Fee";
            Sheet1.Cells["N1"].Value = "Biller Payment";
            Sheet1.Cells["O1"].Value = "Fee Earned";
            Sheet1.Cells["P1"].Value = "Breakage Earned";
            Sheet1.Cells["Q1"].Value = "Service Charges";
            Sheet1.Cells["R1"].Value = "Total Earning";
            Sheet1.Cells["S1"].Value = "Status";
            Sheet1.Cells["T1"].Value = "Repost";
            Sheet1.Cells["U1"].Value = "User";
            Sheet1.Cells["V1"].Value = "Biller Due";
            #endregion
            row = 2;
            currentformula = "";
            foreach (var a in reconciliationReport)
            {
                currentformula = "=(N" + row + "-O" + row + ")";
                Sheet1.Cells[string.Format("A{0}", row)].Value = a.TransactionLogID.ToString(); //
                Sheet1.Cells[string.Format("B{0}", row)].Value = a.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"); //a.SourceTransactionID;
                Sheet1.Cells[string.Format("C{0}", row)].Value = a.ResponseTime != null ? a.ResponseTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                Sheet1.Cells[string.Format("D{0}", row)].Value = a.KioskID.ToString() ?? "N/A"; //a.TransactionCurrency;
                Sheet1.Cells[string.Format("E{0}", row)].Value = a.BillerID.ToString() ?? "N/A"; //
                Sheet1.Cells[string.Format("F{0}", row)].Value = a.ServiceName ?? "N/A";
                Sheet1.Cells[string.Format("G{0}", row)].Value = a.ProductID ?? "N/A";
                Sheet1.Cells[string.Format("H{0}", row)].Value = a.ConsumerID ?? "N/A";
                Sheet1.Cells[string.Format("I{0}", row)].Value = Convert.ToDouble(a.TotalDeposit.ToString() ?? "0");
                Sheet1.Cells[string.Format("J{0}", row)].Value = a.NoteBreakDown ?? "N/A";
                Sheet1.Cells[string.Format("K{0}", row)].Value = a.CashCycleID.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("L{0}", row)].Value = a.BillerReference.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("M{0}", row)].Value = a.Fee.ToString() ?? "0%";
                Sheet1.Cells[string.Format("N{0}", row)].Value = Convert.ToDouble(a.BillerPayment.ToString() ?? "0");
                Sheet1.Cells[string.Format("O{0}", row)].Value = Convert.ToDouble(a.FeeEarned.ToString() ?? "0");
                Sheet1.Cells[string.Format("P{0}", row)].Value = Convert.ToDouble(a.BreakageEarned.ToString() ?? "0");
                Sheet1.Cells[string.Format("Q{0}", row)].Value = Convert.ToInt32(a.ServiceCharges.ToString() ?? "0");
                Sheet1.Cells[string.Format("R{0}", row)].Value = Convert.ToDouble(a.TotalEarning.ToString() ?? "0");
                Sheet1.Cells[string.Format("S{0}", row)].Value = a.Status.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("T{0}", row)].Value = a.Repost.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("U{0}", row)].Value = a.UserID.ToString() ?? "N/A";
                Sheet1.Cells[string.Format("V{0}", row)].Value = Convert.ToDouble(a.Biller_Due.ToString() ?? "0");

                row++;
            }
            Sheet1.Cells["A:AZ"].AutoFitColumns();

            //Add logo
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("~/Content/Image/TGlogo.png")))
            {
                var excelImage = Sheet.Drawings.AddPicture("My Logo", image);

                //add the image to row 2, column B
                excelImage.SetPosition(1, 0, 1, 0);
                excelImage.SetSize(200, 75);
            }

            //define the data range on the source sheet
            var dataRange = Sheet1.Cells[Sheet1.Dimension.Address];
            var dataRange1 = Sheet2.Cells[Sheet2.Dimension.Address];

            //create the pivot table
            var pivotTable = Sheet.PivotTables.Add(Sheet.Cells["B11"], dataRange, "PivotTable");
            var pivotTable1 = Sheet.PivotTables.Add(Sheet.Cells["B33"], dataRange1, "PivotTable1");

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
            var field_product = pivotTable1.Fields[1];
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
            //Heading for tables
            //Heading 1
            ExcelRange rg = Sheet.Cells["B10"];
            rg.IsRichText = true;
            //ExcelRichText uses "using OfficeOpenXml.Style;"
            ExcelRichText text1 = rg.RichText.Add("TG Pay Details");
            text1.Bold = true;
            text1.FontName = "Calibri";
            text1.Size = 18;
            text1.Color = System.Drawing.Color.Black;
            //Heading 2
            ExcelRange h2 = Sheet.Cells["B31"];
            h2.IsRichText = true;
            //ExcelRichText uses "using OfficeOpenXml.Style;"
            ExcelRichText text2 = h2.RichText.Add("Ding Details");
            text2.Bold = true;
            text2.FontName = "Calibri";
            text2.Size = 18;
            text2.Color = System.Drawing.Color.Black;
            //Heading 3
            ExcelRange h4 = Sheet.Cells["B7"];
            h4.IsRichText = true;
            //ExcelRichText uses "using OfficeOpenXml.Style;"
            ExcelRichText text4 = h4.RichText.Add("Settlement Auto Generated Report");
            text4.Bold = true;
            text4.FontName = "Calibri";
            text4.Size = 20;
            text4.Color = System.Drawing.Color.Black;
            //Heading 4
            ExcelRange h5 = Sheet.Cells["B9"];
            h5.IsRichText = true;
            //ExcelRichText uses "using OfficeOpenXml.Style;"
            ExcelRichText text5 = h5.RichText.Add("Month Of " + monthName);
            text5.Bold = true;
            text5.FontName = "Calibri";
            text5.Size = 18;
            text5.Color = System.Drawing.Color.Black;
            //Footer
            ExcelRange g = Sheet.Cells["D50"];
            g.IsRichText = true;
            //ExcelRichText uses "using OfficeOpenXml.Style;"
            ExcelRichText Footer = g.RichText.Add("© 2022 Transguard Pay");
            Footer.Bold = true;
            //Footer.Italic = true;
            Footer.FontName = "Calibri";
            Footer.Size = 11;
            Footer.Color = System.Drawing.Color.Black;
            handle = Guid.NewGuid().ToString();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Ep.SaveAs(memoryStream);
                memoryStream.Position = 0;
                TempData[handle] = memoryStream.ToArray();
            }
            string AutomateFileName = "Settlement Report (" + monthName + "-"+ year + ").xlsx";
            return RedirectToAction("DownloadReconciliationReport", "Report", new { fileGuid = handle, fileName = AutomateFileName });

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