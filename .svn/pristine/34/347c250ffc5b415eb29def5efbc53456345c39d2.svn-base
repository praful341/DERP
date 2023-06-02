using BLL;
using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using DERP.Report.Barcode_Print;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Global = DERP.Class.Global;
namespace DERP.Report
{
    public partial class FrmGReportViewerBand : DevExpress.XtraEditors.XtraForm
    {
        #region Delcaration

        Excel.Application myExcelApplication;
        Excel.Workbook myExcelWorkbook;
        Excel.Worksheet myExcelWorkSheet;

        Dictionary<int, double> dic = new Dictionary<int, double>();

        DataTable DTabDiscount = new DataTable();

        public New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();
        NewReportMaster ObjReport = new NewReportMaster();

        ReportParams ObjReportParams = new ReportParams();

        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();

        string MergeOnStr = string.Empty;
        string MergeOn = string.Empty;
        Boolean ISFilter = false;
        public Boolean IsPivot = false;

        public int ReceiptDays = 0;
        public double DouExpDiff = 0;

        public string BandConsumeCaption = "";
        public string BandConsumeWithProcessCaption = "";

        /// <summary>
        ////
        /// </summary>

        decimal DouOpeningCarat = 0;
        decimal DouOpeningAmount = 0;
        decimal DouClosingCarat = 0;
        decimal DouClosingAmount = 0;
        decimal DouInCarat = 0;
        decimal DouInAmount = 0;
        decimal DouOutCarat = 0;
        decimal DouOutAmount = 0;
        decimal DouMixInCarat = 0;
        decimal DouMixInAmount = 0;
        decimal DouMixOutCarat = 0;
        decimal DouMixOutAmount = 0;
        decimal DouTotalCarat = 0;
        decimal DouTotalAmount = 0;
        decimal DouPrdAmount = 0;
        decimal DouSaleCarat = 0;
        decimal DouSaleAmount = 0;
        decimal DouOnHandCarat = 0;
        decimal DouOnHandAmount = 0;
        decimal DouRealHandCarat = 0;
        decimal DouRealHandAmount = 0;
        decimal DouConfirmCarat = 0;
        decimal DouConfirmAmount = 0;
        decimal DouRapCarat = 0;
        decimal DouRapAmount = 0;
        decimal DouWeightLossCarat = 0;
        decimal DouWeightLossAmount = 0;
        decimal DouWeightPlusCarat = 0;
        decimal DouWeightPlusAmount = 0;
        decimal DouLostCarat = 0;
        decimal DouLostAmount = 0;
        decimal DouLossCarat = 0;
        decimal DouLossAmount = 0;
        decimal DouRejCarat = 0;
        decimal DouRejAmount = 0;
        decimal DouCurrentAmount = 0;
        decimal DouToTotalCarat = 0;
        decimal DouToTotalAmount = 0;
        decimal DouGrossAmount = 0;
        decimal DouPurchaseAmount = 0;
        decimal DouPurchaseAmountTotal = 0;
        decimal DouSalesAmount = 0;
        decimal DouAmount = 0;
        decimal DouDiffAmount = 0;
        decimal DouPL_Per = 0;
        decimal numTotal_Carat = 0;
        decimal numTotal_Amount = 0;
        decimal num_Total_Carat_2 = 0;
        decimal numTotal_Amount_2 = 0;
        decimal num_Total_Carat_3 = 0;
        decimal numTotal_Amount_3 = 0;
        decimal num_Total_Carat_4 = 0;
        decimal numTotal_Amount_4 = 0;
        decimal num_Total_Carat_5 = 0;
        decimal numTotal_Amount_5 = 0;
        decimal num_Total_Carat_6 = 0;
        decimal numTotal_Amount_6 = 0;
        decimal num_Total_Carat_7 = 0;
        decimal numTotal_Amount_7 = 0;
        decimal num_Total_Carat_9 = 0;
        decimal numTotal_Amount_9 = 0;
        decimal num_Total_Carat_10 = 0;
        decimal numTotal_Amount_10 = 0;
        decimal num_Total_Carat2 = 0;
        decimal numTotal_Amount2 = 0;
        decimal num_Total_Carat00 = 0;
        decimal numTotal_Amount00 = 0;
        decimal num_Final_Total_Carat = 0;
        decimal num_Final_Total_Amount = 0;
        decimal num_Actual_Sale_Carat = 0;
        decimal num_Actual_Sale_Amount = 0;
        decimal num_Sale_Amount = 0;
        decimal num_Org_Carat = 0;
        decimal num_Saring_Carat = 0;
        decimal num_4p_Carat = 0;
        decimal num_Polish_Carat = 0;
        decimal num_receive_carat = 0;
        decimal num_issue_carat = 0;
        decimal num_k_carat = 0;
        decimal num_k_pcs = 0;
        decimal num_receive_pcs = 0;
        decimal DouPolishCarat = 0;
        decimal DouKapanCarat = 0;
        decimal num_table_carat = 0;
        decimal DouDiffPcs = 0;
        decimal DouKapanPcs = 0;
        decimal DouCostingAmtPer = 0;
        decimal DouCostingRate = 0;
        decimal DouCostingAvg = 0;
        decimal DouJangedCarat = 0;
        decimal DouJangedAmount = 0;
        decimal DouFinalAmount = 0;
        decimal num_Russion_Carat = 0;
        decimal num_sarin_carat = 0;
        decimal num_polish_carat = 0;
        decimal num_CurrentAmount = 0;
        decimal num_K_Carat = 0;

        decimal DouTotalOrnCarat = 0;
        decimal DouTotalJweCarat = 0;
        decimal DouTotalDlxCarat = 0;
        decimal DouTotalFineCarat = 0;
        decimal DouTotalKhadCarat = 0;
        decimal DouTotalBrakCarat = 0;

        decimal DouP_Carat = 0;
        decimal DouRough_Wt = 0;
        decimal DouSaw_Wt = 0;
        decimal DouPolishPcs = 0;
        decimal DouDifference = 0;
        Int64 makable_pcs = 0;
        Int64 lot_no = 0;
        decimal DouCarat = 0;
        decimal total_carat_dollar = 0;

        decimal Total_Polish_Dollar = 0;
        decimal DouTotalLabourAmount = 0;
        decimal P_Carat = 0;
        decimal Total_Dollar = 0;
        decimal est_wt = 0;
        decimal xyz = 0;
        //int DouCountLot = 0;
        //string Lot_No = "";
        //string Lot_No_New = "";


        #region Property Settings

        private DataTable _mDTDetail = new DataTable();

        public DataTable mDTDetail
        {
            get { return _mDTDetail; }
            set { _mDTDetail = value; }
        }

        private DataTable _DTab = new DataTable();

        public DataTable DTab
        {
            get { return _DTab; }
            set { _DTab = value; }
        }

        private string _Group_By_Tag;

        public string Group_By_Tag
        {
            get { return _Group_By_Tag; }
            set { _Group_By_Tag = value; }
        }

        private string _Group_By_Text;

        public string Group_By_Text
        {
            get { return _Group_By_Text; }
            set { _Group_By_Text = value; }
        }

        private bool _BoolShowLabourRate;

        public bool BoolShowLabourRate
        {
            get { return _BoolShowLabourRate; }
            set { _BoolShowLabourRate = value; }
        }

        private string _Order_By;

        public string Order_By
        {
            get { return _Order_By; }
            set { _Order_By = value; }
        }

        private string _ReportHeaderName;

        public string ReportHeaderName
        {
            get { return _ReportHeaderName; }
            set { _ReportHeaderName = value; }
        }

        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        private int _Report_Code;

        public int Report_Code
        {
            get { return _Report_Code; }
            set { _Report_Code = value; }
        }

        private string _Report_Type;

        public string Report_Type
        {
            get { return _Report_Type; }
            set { _Report_Type = value; }
        }

        private string _FormToBeOpen;

        public string FormToBeOpen
        {
            get { return _FormToBeOpen; }
            set { _FormToBeOpen = value; }
        }

        private string _FilterBy;

        public string FilterBy
        {
            get { return _FilterBy; }
            set { _FilterBy = value; }
        }

        public string R_Date;
        public string Shape_Name;
        public string Article_Name;
        public string MSize_Name;

        private ReportParams_Property _ReportParams_Property;
        public ReportParams_Property ReportParams_Property
        {
            get { return _ReportParams_Property; }
            set { _ReportParams_Property = value; }
        }

        private string _Procedure_Name;

        public string Procedure_Name
        {
            get { return _Procedure_Name; }
            set { _Procedure_Name = value; }
        }

        private bool _ChkExportAsaCostingPattern;
        public bool ChkExportAsaCostingPattern
        {
            get { return _ChkExportAsaCostingPattern; }
            set { _ChkExportAsaCostingPattern = value; }
        }
        private string _FromIssue_Date;
        public string FromIssue_Date
        {
            get { return _FromIssue_Date; }
            set { _FromIssue_Date = value; }
        }
        private string _ToIssue_Date;
        public string ToIssue_Date
        {
            get { return _ToIssue_Date; }
            set { _ToIssue_Date = value; }
        }
        private string _FilterByFormName;
        public string FilterByFormName
        {
            get { return _FilterByFormName; }
            set { _FilterByFormName = value; }
        }
        #endregion        

        #endregion


        #region Constructor
        public FrmGReportViewerBand()
        {
            InitializeComponent();

            DTabDiscount.Columns.Add("key");
            DTabDiscount.Columns.Add("value");
        }
        public FrmGReportViewerBand(DataTable pDTab, string pStrOrderBy, string pStrGroupBy, string pStrReportName, int pIntReportCode)
        {
            InitializeComponent();

            DTab = pDTab;
            Group_By_Tag = pStrGroupBy;
            Order_By = pStrOrderBy;
            ReportHeaderName = pStrReportName;
            Report_Code = pIntReportCode;
        }
        public void ShowForm()
        {
            ObjPer.Report_Code = Report_Code;
            AttachFormEvents();

            if (GlobalDec.gEmployeeProperty.user_name == "TUSHAR" || GlobalDec.gEmployeeProperty.user_name == "GAURAV" || GlobalDec.gEmployeeProperty.user_name == "RIKITA" || GlobalDec.gEmployeeProperty.user_name == "YASHY")
            {
                lblReportHeader.Text = ReportHeaderName;
                float currentSize = 15.0F;
                lblReportHeader.Font = new Font(lblReportHeader.Font.Name, currentSize,
                      lblReportHeader.Font.Style, lblReportHeader.Font.Unit);
            }
            else
            {
                lblReportHeader.Text = ReportHeaderName;
            }

            // lblReportHeader.Size = new Size(1000, 300);
            //lblReportHeader.AutoSize = true;

            lblGroupBy.Text = Group_By_Text;

            if (lblGroupBy.Text == "")
            {
                label2.Visible = false;
            }
            else
            {
                label2.Visible = true;
            }
            lblGroupBy.Tag = Group_By_Tag;
            lblFilter.Text = FilterBy;
            this.Text = lblReportHeader.Text + " With Group : " + lblGroupBy.Text;
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Menu Events
        private void MNUExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToExcel_Click(object sender, EventArgs e)
        {
            Export("xls", GridView1);
        }
        public void Export(string format, GridView gvExportGrid, bool isHeaderPrint = true)
        {
            try
            {
                if (gvExportGrid.RowCount < 1)
                {
                    Global.Message("No Rows to Export");
                    return;
                }
                string dlgHeader = string.Empty;
                string dlgFilter = string.Empty;
                format = format.ToLower();
                if (format.Equals("xls"))
                {
                    dlgHeader = "Export to Excel";
                    dlgFilter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                else if (format.Equals("xlsx"))
                {
                    dlgHeader = "Export to XLSX";
                    dlgFilter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                gvExportGrid.OptionsPrint.AutoWidth = false;
                gvExportGrid.OptionsPrint.PrintHeader = isHeaderPrint;

                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                string Filepath = string.Empty;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            gvExportGrid.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            DevExpress.XtraPrinting.XlsExportOptionsEx op = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                            //op.ExportType = DevExpress.Export.ExportType.Default;
                            op.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gvExportGrid.OptionsPrint.UsePrintStyles = false;
                            gvExportGrid.ExportToXls(Filepath, op);
                            break;
                        case "rtf":
                            gvExportGrid.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            gvExportGrid.ExportToText(Filepath);
                            break;
                        case "html":
                            gvExportGrid.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            gvExportGrid.ExportToCsv(Filepath);
                            break;
                        case "xlsx":
                            DevExpress.XtraPrinting.XlsxExportOptionsEx opx = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
                            //opx.ExportType = DevExpress.Export.ExportType.Default;
                            opx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gvExportGrid.OptionsPrint.UsePrintStyles = false;
                            gvExportGrid.ExportToXlsx(Filepath, opx);
                            break;
                    }
                }

                myExcelApplication = null;

                myExcelApplication = new Excel.Application();
                myExcelApplication.DisplayAlerts = false;

                myExcelWorkbook = (Excel.Workbook)(myExcelApplication.Workbooks._Open(Filepath, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                   System.Reflection.Missing.Value, System.Reflection.Missing.Value));

                int numberOfWorkbooks = myExcelApplication.Workbooks.Count;
                myExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.Worksheets.get_Item(1);

                Excel.Range line = (Excel.Range)myExcelWorkSheet.Rows[1];
                line.Insert();
                myExcelWorkSheet.Cells[1, 1] = lblReportHeader.Text;
                Excel.Range line1 = (Excel.Range)myExcelWorkSheet.Rows[2];
                line1.Insert();
                myExcelWorkSheet.Cells[2, 1] = "Group :- " + lblGroupBy.Text;
                Excel.Range line2 = (Excel.Range)myExcelWorkSheet.Rows[3];
                line2.Insert();
                myExcelWorkSheet.Cells[3, 1] = "Parameters :- " + lblFilter.Text;
                Excel.Range line3 = (Excel.Range)myExcelWorkSheet.Rows[4];
                line3.Insert();
                myExcelWorkSheet.Cells[4, 1] = "Print Date :- " + lblDateTime.Text;

                myExcelWorkSheet.Range["A1"].WrapText = false;
                myExcelWorkSheet.Range["A2"].WrapText = false;
                myExcelWorkSheet.Range["A3"].WrapText = false;
                myExcelWorkSheet.Range["A4"].WrapText = false;

                try
                {
                    myExcelWorkbook.SaveAs(Filepath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                                                   System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                                   System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                    myExcelWorkbook.Close(true, Filepath, System.Reflection.Missing.Value);
                }
                finally
                {
                    if (myExcelApplication != null)
                    {
                        myExcelApplication.Quit();
                    }
                }

                if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(Filepath);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        private void ToExcelx_Click(object sender, EventArgs e)
        {
            Export("xlsx", GridView1);
            //Global.Export("xlsx", GridView1);
        }
        private void ToText_Click(object sender, EventArgs e)
        {
            Global.Export("txt", GridView1);
        }
        private void ToHTML_Click(object sender, EventArgs e)
        {
            Global.Export("html", GridView1);
        }
        private void ToRTF_Click(object sender, EventArgs e)
        {
            Global.Export("rtf", GridView1);
        }
        private void ToPDF_Click(object sender, EventArgs e)
        {
            //Global.Export("pdf", GridView1);
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView1.BestFitColumns();
        }
        private void ExpandTool_Click(object sender, EventArgs e)
        {
            GridView1.ExpandAllGroups();
        }
        private void Collapse_Click(object sender, EventArgs e)
        {
            GridView1.CollapseAllGroups();
        }
        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

                PrinterSettingsUsing pst = new PrinterSettingsUsing();

                PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

                PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

                link.Component = GridControl1;

                foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
                {
                    if (Val.ToString(CmbPageKind.SelectedItem) == foo.ToString())
                    {
                        link.PaperKind = foo;
                        link.PaperName = foo.ToString();
                    }
                }

                if (Val.ToString(cmbOrientation.SelectedItem) == "Landscape")
                {
                    link.Landscape = true;
                }
                if (Val.ToString(cmbExpand.SelectedItem) == "Yes")
                {
                    GridView1.OptionsPrint.ExpandAllGroups = true;
                }
                else
                {
                    GridView1.OptionsPrint.ExpandAllGroups = false;
                }

                GridView1.OptionsPrint.AutoWidth = true;

                link.Margins.Left = 40;
                link.Margins.Right = 40;
                link.Margins.Bottom = 40;
                link.Margins.Top = 130;
                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
                link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);
                link.CreateDocument();
                link.ShowPreview();
                link.PrintDlg();
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }
        }

        #endregion

        #region Form Events
        private void FrmGReportViewer_Load(object sender, EventArgs e)
        {
            int IntIndex = 0;
            int IntSelectedIndex = 0;
            CmbPageKind.Items.Clear();
            foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
            {
                CmbPageKind.Items.Add(foo.ToString());

                IntIndex++;
            }
            CmbPageKind.SelectedIndex = IntSelectedIndex;
            lblDateTime.Text = DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt");
            FillGrid();


            FooterSummary();
            if (ObjReportDetailProperty.Remark == "POLISH_CONSUME_VALUATION")
            {
                TsmExportData.Visible = true;
            }
            if (FilterByFormName != null && !FilterByFormName.Trim().Equals(string.Empty))
            {
                if (
                    Val.ToString(FilterByFormName).Equals("FrmMixIssueReceiveFilterBy")
                    || Val.ToString(FilterByFormName).Equals("FrmMixIssueReceiveFilterByNew")
                   )
                {
                    pnlRefresh.Visible = true;
                }
            }
        }

        #endregion

        #region Other Function
        void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel, string Filepath)
        {
            PrintableComponentLinkBase link = new PrintableComponentLinkBase()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true

            };

            link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

            link.ExportToXls(Filepath);
        }
        void PreviewPrintableComponent_PDF(IPrintable component, UserLookAndFeel lookAndFeel, string Filepath)
        {
            PrintableComponentLinkBase link = new PrintableComponentLinkBase()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true

            };

            link.CreateReportHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);

            link.ExportToPdf(Filepath);
        }
        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            GridView1.OptionsPrint.ExpandAllDetails = true;
            DevExpress.XtraGrid.Export.GridViewExportLink gvlink;
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    switch (format)
                    {
                        case "pdf":
                            PreviewPrintableComponent_PDF(GridControl1, GridControl1.LookAndFeel, Filepath);
                            // GridView1.ExportToPdf(Filepath);

                            break;
                        case "xls":
                            PreviewPrintableComponent(GridControl1, GridControl1.LookAndFeel, Filepath);

                            break;
                        case "xlsx":

                            PreviewPrintableComponent(GridControl1, GridControl1.LookAndFeel, Filepath);
                            break;
                        case "rtf":
                            GridView1.ExportToRtf(Filepath);
                            break;
                        case "txt":

                            gvlink = (DevExpress.XtraGrid.Export.GridViewExportLink)GridView1.CreateExportLink(new DevExpress.XtraExport.ExportTxtProvider(Filepath));

                            gvlink.ExportAll = true;

                            gvlink.ExpandAll = true;

                            gvlink.ExportDetails = true;

                            gvlink.ExportTo(true);
                            break;
                        case "html":

                            gvlink = (DevExpress.XtraGrid.Export.GridViewExportLink)GridView1.CreateExportLink(new DevExpress.XtraExport.ExportHtmlProvider(Filepath));

                            gvlink.ExportAll = true;

                            gvlink.ExpandAll = true;

                            gvlink.ExportDetails = true;

                            gvlink.ExportTo(true);
                            break;
                    }
                    if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Filepath);
                    }
                    //if (Global.Confirm("Press Yes To Open the File.") == System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    System.Diagnostics.Process.Start(Filepath, "CMD");
                    //}
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        public void FillGrid()
        {
            int IntError = 0;

            try
            {
                DataView dv = new DataView(mDTDetail);
                dv.Sort = "Sequence_No";
                mDTDetail = dv.ToTable();

                foreach (DataRow DRow in mDTDetail.Rows)
                {
                    if (Val.ToBoolean(DRow["visible"].ToString()) == false)
                    {
                    }
                    else
                    {
                        if (DRow["mergeon"].ToString() != "")
                        {
                            MergeOn = DRow["mergeon"].ToString();

                            if (MergeOnStr == "")
                            {
                                MergeOnStr = DRow["mergeon"].ToString();
                            }
                            else
                            {
                                MergeOnStr = MergeOnStr + "," + DRow["field_name"].ToString();
                            }
                        }
                    }
                }
                List<GridBand> AL = new List<GridBand>();
                DataView view = new DataView(mDTDetail);
                DataTable distinctValues = view.ToTable(true, "bands");

                foreach (DataRow DRow in distinctValues.Rows)
                {
                    var gridBand = new GridBand();
                    gridBand.Caption = Val.ToString(DRow["bands"]);
                    gridBand.RowCount = 2;
                    AL.Add(gridBand);
                }

                GridControl1.DataSource = DTab;
                GridView1.OptionsView.AllowCellMerge = true;

                GridView1.Bands.AddBand(lblReportHeader.Text);


                //GridGroupSummaryItem item = new GridGroupSummaryItem();
                //item.FieldName = "TOTAL_LOT";
                //item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item.ShowInGroupColumnFooter = GrdDet.Columns["TOTAL_LOT"];
                //GrdDet.GroupSummary.Add(item);

                //GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                //item1.FieldName = "TOTAL_PCS";
                //item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item1.ShowInGroupColumnFooter = GrdDet.Columns["TOTAL_PCS"];
                //GrdDet.GroupSummary.Add(item1);

                //GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                //item2.FieldName = "TOTAL_CARAT";
                //item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                //item2.ShowInGroupColumnFooter = GrdDet.Columns["TOTAL_CARAT"];
                //GrdDet.GroupSummary.Add(item2);

                foreach (DataRow DRow in mDTDetail.Rows)
                {
                    if (Val.ToBoolean(DRow["visible"].ToString()) == true && Val.ToBoolean(DRow["is_unbound"]) == true)
                    {
                        DevExpress.XtraGrid.Columns.GridColumn unbColumn = GridView1.Columns.AddField(Val.ToString(DRow["field_name"]));
                        unbColumn.VisibleIndex = Val.ToInt(DRow["sequence_no"]);
                        unbColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                        unbColumn.Caption = Val.ToString(DRow["column_name"]);
                        unbColumn.Tag = DRow["field_name"].ToString();
                        unbColumn.OptionsColumn.AllowEdit = false;

                        unbColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                        if (Val.ToString(DRow["format"]).ToUpper() == "N1")
                        {
                            unbColumn.DisplayFormat.FormatString = "###################0.0";
                        }
                        if (Val.ToString(DRow["format"]).ToUpper() == "N2")
                        {
                            unbColumn.DisplayFormat.FormatString = "###################0.00";
                        }
                        else if (Val.ToString(DRow["format"]).ToUpper() == "N3")
                        {
                            unbColumn.DisplayFormat.FormatString = "###################0.000";
                        }
                        else if (Val.ToString(DRow["format"]).ToUpper() == "N4")
                        {
                            unbColumn.DisplayFormat.FormatString = "###################0.0000";
                        }
                        unbColumn.UnboundExpression = Val.ToString(DRow["expression"]);
                        unbColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    }
                    else
                    {

                        bool iBool = false;
                        foreach (DataColumn DCol in DTab.Columns)
                        {
                            if (DCol.ColumnName == DRow["field_name"].ToString())
                            {
                                iBool = true;
                                break;
                            }
                        }

                        if (iBool == false)
                        {
                            continue;
                        }

                        if (Val.ToBoolean(DRow["visible"].ToString()) == false)
                        {
                            GridView1.Columns[DRow["field_name"].ToString()].Visible = false;
                            continue;
                        }

                        if (Val.ToBoolean(DRow["ismerge"].ToString()) == false)
                        {
                            GridView1.Columns[DRow["field_name"].ToString()].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                        }
                        if (Val.ToInt64(DRow["width"].ToString()) != 0)
                        {
                            GridView1.Columns[DRow["field_name"].ToString()].Width = Val.ToInt(DRow["width"].ToString());
                        }

                        //Set Column Caption
                        GridView1.Columns[DRow["field_name"].ToString()].Caption = DRow["column_name"].ToString();
                        GridView1.Columns[DRow["field_name"].ToString()].Tag = DRow["field_name"].ToString();
                        GridView1.Columns[DRow["field_name"].ToString()].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                    }

                    string StrFormat = string.Empty;
                    string StrSummryFormat = string.Empty;

                    switch (DRow["type"].ToString().ToUpper())
                    {
                        case "I":
                            StrFormat = "###################0";
                            StrSummryFormat = "{0:N0}";

                            break;
                        case "F":
                            if (Val.ToString(DRow["format"]).ToUpper() == "N0")
                            {
                                StrFormat = "###################0";
                            }
                            if (Val.ToString(DRow["format"]).ToUpper() == "N1")
                            {
                                StrFormat = "###################0.0";
                            }
                            if (Val.ToString(DRow["format"]).ToUpper() == "N2")
                            {
                                StrFormat = "###################0.00";
                            }
                            else if (Val.ToString(DRow["format"]).ToUpper() == "N3")
                            {
                                StrFormat = "###################0.000";
                            }
                            else if (Val.ToString(DRow["format"]).ToUpper() == "N4")
                            {
                                StrFormat = "###################0.0000";
                            }
                            StrSummryFormat = "{0:" + Val.ToString(DRow["format"]).ToUpper() + "}";
                            break;
                        case "D":
                            StrFormat = "dd-MMM-yyyy";
                            break;
                        case "T":
                            StrFormat = "hh:mm tt";
                            break;
                        default:
                            StrFormat = "";
                            break;
                    }
                    /* Add Alignment */
                    switch (DRow["alignment"].ToString().ToUpper())
                    {
                        case "left":
                            GridView1.Columns[DRow["field_name"].ToString()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                            break;
                        case "right":
                            GridView1.Columns[DRow["field_name"].ToString()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            break;
                        case "center":
                            GridView1.Columns[DRow["field_name"].ToString()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            break;
                        default:
                            GridView1.Columns[DRow["field_name"].ToString()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
                            break;
                    }

                    /* Set Order */
                    switch (DRow["order_by"].ToString().ToUpper())
                    {
                        case "ASC":
                            GridView1.Columns[DRow["field_name"].ToString()].SortOrder = ColumnSortOrder.Ascending;
                            break;
                        case "DESC":
                            GridView1.Columns[DRow["field_name"].ToString()].SortOrder = ColumnSortOrder.Descending;
                            break;
                        default:
                            GridView1.Columns[DRow["field_name"].ToString()].SortOrder = ColumnSortOrder.None;
                            break;
                    }

                    GridView1.Columns[DRow["field_name"].ToString()].OptionsColumn.AllowEdit = false;
                    GridView1.Columns[DRow["field_name"].ToString()].DisplayFormat.FormatString = StrFormat;
                    GridView1.Columns[DRow["field_name"].ToString()].VisibleIndex = Val.ToInt(DRow["sequence_no"]);

                    foreach (GridBand band in AL)
                    {
                        if (band.Caption == Val.ToString(DRow["bands"]))
                        {
                            GridView1.Columns[DRow["field_name"].ToString()].OwnerBand = band;

                            bool ISExists = false;

                            foreach (GridBand band1 in GridView1.Bands[0].Children)
                            {
                                if (band1.Caption == band.Caption)
                                {
                                    ISExists = true;
                                    break;
                                }
                            }

                            if (ISExists == false)
                            {
                                GridView1.Bands[0].Children.Add(band);
                                GridView1.Bands[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                GridView1.Columns[DRow["field_name"].ToString()].OwnerBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            }
                            break;
                        }
                    }

                    // Set Summry Field and group Summry Also

                    if (Val.ToBoolean(DRow["visible"].ToString()) == true && Val.ToBoolean(DRow["ismerge"].ToString()) == false)
                    {
                        switch (DRow["aggregate"].ToString().ToUpper())
                        {
                            case "SUM":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Sum, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Sum, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);
                                break;
                            case "AVG":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Average, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Average, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);
                                break;
                            case "COUNT":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Count, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Count, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);
                                break;
                            case "MAX":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Max, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Max, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);
                                break;
                            case "MIN":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Min, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Min, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);
                                break;
                            case "WEI.AVG":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Custom, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Custom, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);
                                break;
                            case "CUSTOME":
                                GridView1.Columns[DRow["field_name"].ToString()].Summary.Add(SummaryItemType.Custom, DRow["field_name"].ToString(), StrSummryFormat);
                                GridView1.GroupSummary.Add(SummaryItemType.Custom, DRow["field_name"].ToString(), GridView1.Columns[DRow["field_name"].ToString()], StrSummryFormat);

                                break;
                            default:
                                break;
                        }
                    }
                }

                //Group By Setting
                GridView1.ClearSorting();

                string[] StrGroupBy = new string[] { }; if (Group_By_Tag != null) StrGroupBy = Group_By_Tag.Split(',');

                int IntCount = 0;

                if (IsPivot == false)
                {
                    if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEMAND_NOTING_REPORT")
                    {
                        IntCount = StrGroupBy.Length;
                    }
                    else
                    {
                        if (Val.ToString(Report_Type).ToUpper().Contains("SUMMARY"))
                        {
                            IntCount = StrGroupBy.Length - 1;
                        }
                        else
                        {
                            IntCount = StrGroupBy.Length;
                        }
                    }
                    for (int IntI = 0; IntI < IntCount; IntI++)
                    {
                        if (StrGroupBy[IntI] != "")
                        {
                            GridView1.Columns[StrGroupBy[IntI]].GroupIndex = IntI;
                            GridView1.Columns[StrGroupBy[IntI]].Group();
                        }
                    }

                    if (Group_By_Tag == "")
                    {
                        foreach (string Str in Val.ToString(Order_By).Split(','))
                        {
                            if (Str != "")
                            {
                                GridView1.Columns[Str].SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
                                GridView1.Columns[Str].SortOrder = ColumnSortOrder.Ascending;
                            }
                        }
                    }
                }

                string[] StrCaption = BandConsumeCaption.Split(',');
                string[] StrCaptionValue = BandConsumeWithProcessCaption.Split(',');

                if (StrCaptionValue.Length == StrCaption.Length)
                {
                    foreach (GridBand gridBand in GridView1.Bands[0].Children)
                    {
                        for (int IntI = 0; IntI < StrCaption.Length; IntI++)
                        {
                            if (StrCaption[IntI].ToUpper() == gridBand.Caption.ToUpper())
                            {
                                gridBand.Caption = StrCaptionValue[IntI];
                            }
                        }

                        if (BoolShowLabourRate == true)
                        {
                            if (gridBand.Caption.ToUpper() == "LABOUR")
                            {
                                gridBand.Visible = true;
                            }
                        }
                        else
                        {
                            if (gridBand.Caption.ToUpper() == "LABOUR")
                            {
                                gridBand.Visible = false;
                            }
                        }
                    }
                }

                GridView1.Appearance.Row.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Regular);
                GridView1.AppearancePrint.Row.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Regular);

                GridView1.Appearance.BandPanel.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Bold);
                GridView1.AppearancePrint.BandPanel.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Bold);

                GridView1.Appearance.HeaderPanel.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Bold);
                GridView1.AppearancePrint.HeaderPanel.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Bold);

                GridView1.Appearance.GroupRow.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Bold);
                GridView1.AppearancePrint.GroupRow.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size)), FontStyle.Bold);

                GridView1.Appearance.GroupFooter.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size - 1)), FontStyle.Bold);
                GridView1.AppearancePrint.GroupFooter.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size - 1)), FontStyle.Bold);

                GridView1.Appearance.FooterPanel.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size - 1)), FontStyle.Bold);
                GridView1.AppearancePrint.FooterPanel.Font = new Font(ObjReportDetailProperty.Font_Name, float.Parse(Val.ToString(ObjReportDetailProperty.Font_Size - 1)), FontStyle.Bold);

                GridView1.OptionsPrint.UsePrintStyles = true;
                GridView1.OptionsSelection.MultiSelect = true;
                GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
                cmbOrientation.SelectedItem = ObjReportDetailProperty.Page_Orientation;
                GridView1.ExpandAllGroups();
                //GridView1.BestFitColumns();

                if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "RPT_LIVE_STOCK_WITH_REJECTION")
                {
                    GridView1.OptionsView.ShowFooter = false;
                }
                else if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEMAND_NOTING_REPORT")
                {
                    GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                    item4.FieldName = "mapColor";
                    GridView1.GroupSummary.Add(item4);

                    GridGroupSummaryItem item5 = new GridGroupSummaryItem();
                    item5.FieldName = "sieve_name";
                    GridView1.GroupSummary.Add(item5);

                    GridGroupSummaryItem item6 = new GridGroupSummaryItem();
                    item6.FieldName = "size_group";
                    GridView1.GroupSummary.Add(item6);

                }
                if (Val.ToString(ReportHeaderName) == "Diameter + Purity wise Summary Report")
                {
                    //GridView1.Columns["clarity_name"].Fixed = FixedStyle.Left;
                    //GridView1.ClearGrouping();
                    GridView1.Columns["diameter_range"].GroupIndex = 0;
                    GridView1.OptionsView.ShowGroupedColumns = false;
                    GridView1.ExpandAllGroups();
                }
            }
            catch (Exception Ex)
            {
                Global.Message("Error In Column Index : " + IntError.ToString() + "    " + Ex.Message);
            }
        }
        private void SetGridBand(BandedGridView bandedView, string gridBandCaption, string[] columnNames)
        {
            var gridBand = new GridBand();
            gridBand.Caption = gridBandCaption;
            int nrOfColumns = columnNames.Length;
            BandedGridColumn[] bandedColumns = new BandedGridColumn[nrOfColumns];
            for (int i = 0; i < nrOfColumns; i++)
            {
                bandedColumns[i] = (BandedGridColumn)bandedView.Columns.AddField(columnNames[i]);
                bandedColumns[i].OwnerBand = gridBand;
                bandedColumns[i].Visible = true;
            }
            bandedView.Bands.Add(gridBand);
        }
        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title
            TextBrick BrickTitle = e.Graph.DrawString(lblReportHeader.Text, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Tahoma", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Group :- " + lblGroupBy.Text, System.Drawing.Color.Navy, new RectangleF(0, 25, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("Tahoma", 8, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            TextBrick BrickTitlesParam = e.Graph.DrawString("Parameters :- " + lblFilter.Text, System.Drawing.Color.Navy, new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 60), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("Tahoma", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + lblDateTime.Text, System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Tahoma", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }
        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            BrickPageNo.Font = new Font("Tahoma", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }
        public double GenerateTimeFieldSummry(GridView view, string Field)
        {
            if (view == null) return 0;

            if (Val.ToString(Field) == "") return 0;

            GridColumn TimetCol = view.Columns[Field];

            if (TimetCol == null) return 0;

            try
            {
                double totalWeight = 0;

                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (view.IsNewItemRow(i)) continue;

                    object temp;

                    double weight;

                    if (view.IsGroupRow(i))
                    {
                        temp = view.GetRowCellValue(i, TimetCol);
                    }
                    else
                    {
                        temp = view.GetRowCellValue(i, TimetCol);
                    }
                    temp = view.GetRowCellValue(i, TimetCol);
                    weight = (temp == DBNull.Value || temp == null) ? 0 : Val.Val(temp);
                    totalWeight += weight;
                }

                if (totalWeight == 0) return 0;

                string[] parts = totalWeight.ToString().Split('.');
                int i1 = Val.ToInt(parts[0]);
                int i2 = Val.ToInt(parts[1]);

                while (i2 > 60)
                {
                    i1 = i1 + 1;
                    i2 = i2 - 60;
                }
                return Val.Val(i1.ToString() + "." + i2.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public double GetWeightedAverage(GridView view, string weightField, string valueField)
        {
            if (view == null) return 0;

            if (Val.ToString(weightField) == "" || Val.ToString(valueField) == "") return 0;

            GridColumn weightCol = view.Columns[weightField];

            GridColumn valueCol = view.Columns[valueField];

            if (weightCol == null || valueCol == null) return 0;

            try
            {
                double totalWeight = 0, totalValue = 0;

                for (int i = 0; i < view.DataRowCount; i++)
                {

                    if (view.IsNewItemRow(i)) continue;

                    object temp;

                    double weight, val;

                    temp = view.GetRowCellValue(i, weightCol);
                    weight = (temp == DBNull.Value || temp == null) ? 0 : Val.Val(temp);
                    temp = view.GetRowCellValue(i, valueCol);
                    val = (temp == DBNull.Value || temp == null) ? 0 : Val.Val(temp);
                    totalWeight += weight;
                    totalValue += weight * val;
                }
                if (totalWeight == 0) return 0;
                return Val.Val(totalValue / totalWeight);
            }
            catch
            {
                return 0;
            }
        }
        private void SetColumnsOrder(DataTable table, params String[] columnNames)
        {
            try
            {
                int columnIndex = 0;
                foreach (var columnName in columnNames)
                {
                    if (table.Columns.Contains(columnName))
                    {
                        table.Columns[columnName].SetOrdinal(columnIndex);
                        columnIndex++;
                    }
                }
            }
            catch (Exception Ex)
            { }
        }
        public void GridOrderByData(ref DataTable Dt)
        {
            GridColumnSortInfoCollection str = GridView1.SortInfo;
            int i = 0;
            int Count = str.Count();
            String FilterValue = "";
            String OrderBy = "";

            foreach (GridColumnSortInfo col in str)
            {
                i++;

                OrderBy = col.Column.SortOrder.ToString().ToUpper() == "ASCENDING" ? " ASC" : " DESC";

                if (Count == i)
                {
                    FilterValue += col.Column.FieldName + OrderBy;
                }
                else
                {
                    FilterValue += col.Column.FieldName + OrderBy + ",";
                }
            }
            Dt.DefaultView.Sort = FilterValue;
            Dt = Dt.DefaultView.ToTable();
            Dt.AcceptChanges();
        }
        public void GetGridFooterValue(ref DataTable Dt1)
        {
            try
            {
                DataRow DRow = Dt1.NewRow();
                string Str1 = "";
                foreach (GridColumn Column in GridView1.VisibleColumns)
                {
                    if (GridView1.Columns[Column.FieldName].SummaryText.ToString() != "")
                    {
                        if (Dt1.Columns.Contains(Column.FieldName))
                        {
                            DRow[Column.FieldName] = GridView1.Columns[Column.FieldName].SummaryText;
                            Str1 += DRow[Column.FieldName].ToString();
                        }
                    }
                }
                Dt1.Rows.Add(DRow);
            }
            catch (Exception Ex)
            { }
        }
        private void GetActiveFilterAndApply(ref DataTable Dt)
        {
            try
            {
                String FilterData = "";
                Int32 ActiveCriteAriaCount = GridView1.ActiveFilter.Count;
                int i = 1;
                foreach (ViewColumnFilterInfo info in GridView1.ActiveFilter)
                {
                    if (i == ActiveCriteAriaCount)
                        FilterData += info.Column.FilterInfo.FilterCriteria.ToString();
                    else
                        FilterData += info.Column.FilterInfo.FilterCriteria.ToString() + " AND ";
                    i++;
                }

                Dt = Dt.Select(String.Format("{0}", FilterData)).CopyToDataTable();

                Dt.AcceptChanges();
            }
            catch (Exception Ex)
            {
            }
        }
        private DataTable ConvertDataTableDataType(DataTable Dt1, DataTable destTable)
        {
            DataTable DTab1 = destTable;
            try
            {
                Dt1.AsEnumerable().ToList().ForEach(row => DTab1.ImportRow(row));
            }
            catch (Exception ex)
            {
            }
            return DTab1;
        }
        public int GetGroupSummryIndex(string pStrFieldName)
        {
            int IntIndex = 0;
            foreach (GridGroupSummaryItem item in GridView1.GroupSummary)
            {
                if (item.FieldName.ToUpper() == pStrFieldName)
                {
                    IntIndex = item.Index;
                    break;
                }
            }
            return IntIndex;
        }
        private double GetPercent(int rowHandle, string pStrFieldName)
        {
            int IntIndex = GetGroupSummryIndex(pStrFieldName);
            int groupRow = GridView1.GetParentRowHandle(rowHandle);

            double part = 0;
            double total = 0;

            if (GridView1.IsGroupRow(groupRow))
            {
                if (pStrFieldName == "RR_CARAT")
                {
                    part = Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                    total = Val.Val(GridView1.GetRowCellValue(rowHandle, "CONSUME_CARAT"));
                    total += Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                }
                else if (pStrFieldName == "MAJOR_CARAT" || pStrFieldName == "MINOR_CARAT")
                {
                    part = Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                    total = Val.Val(GridView1.GetGroupSummaryValue(groupRow, GridView1.GroupSummary[GetGroupSummryIndex("CONSUME_CARAT")] as DevExpress.XtraGrid.GridGroupSummaryItem));
                }
                else
                {
                    total = Val.Val(GridView1.GetGroupSummaryValue(groupRow, GridView1.GroupSummary[IntIndex] as DevExpress.XtraGrid.GridGroupSummaryItem));
                    part = Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                }
            }
            else
            {
                if (pStrFieldName == "RR_CARAT")
                {
                    part = Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                    total = Val.Val(GridView1.GetRowCellValue(rowHandle, "CONSUME_CARAT"));
                    total += Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                }
                else if (pStrFieldName == "MAJOR_CARAT" || pStrFieldName == "MINOR_CARAT")
                {
                    part = Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                    total = Val.Val(GridView1.Columns["CONSUME_CARAT"].Summary[0].SummaryValue);
                }
                else
                {
                    total = Val.Val(GridView1.Columns[pStrFieldName].Summary[0].SummaryValue);
                    part = Val.Val(GridView1.GetRowCellValue(rowHandle, pStrFieldName));
                }
            }
            return (total == 0) ? 0 : (part / total) * 100;
        }
        public void GetGroupRowPercentage(object sender, CustomSummaryEventArgs e, string pStrFieldName)
        {
            GridView view = sender as GridView;
            int IntIndex = GetGroupSummryIndex(pStrFieldName);

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                int IntParentSummryRowHandle = 0;
                int IntCurrentGroupRowHandle = 0;
                double total = 0;
                double part = 0;

                if (e.GroupLevel == -1)
                {
                    if (pStrFieldName == "RR_CARAT")
                    {
                        part = Val.Val(view.Columns[pStrFieldName].Summary[0].SummaryValue);
                        total = Val.Val(view.Columns[pStrFieldName].Summary[0].SummaryValue);
                        total += Val.Val(view.Columns["CONSUME_CARAT"].Summary[0].SummaryValue);
                    }
                    else if (pStrFieldName == "MAJOR_CARAT" || pStrFieldName == "MINOR_CARAT")
                    {
                        part = Val.Val(view.Columns[pStrFieldName].Summary[0].SummaryValue);
                        total = Val.Val(view.Columns["CONSUME_CARAT"].Summary[0].SummaryValue);
                    }
                    else
                    {
                        part = Val.Val(view.Columns[pStrFieldName].Summary[0].SummaryValue);
                        total = Val.Val(view.Columns[pStrFieldName].Summary[0].SummaryValue);
                    }
                }

                else if (e.GroupLevel == 0)
                {
                    IntCurrentGroupRowHandle = e.GroupRowHandle;

                    if (pStrFieldName == "RR_CARAT")
                    {
                        part = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        total = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        IntIndex = GetGroupSummryIndex("CONSUME_CARAT");
                        total += Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                    }
                    else if (pStrFieldName == "MAJOR_CARAT" || pStrFieldName == "MINOR_CARAT")
                    {
                        part = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        total = Val.Val(view.Columns["CONSUME_CARAT"].Summary[0].SummaryValue);
                    }
                    else
                    {
                        part = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        total = Val.Val(view.Columns[pStrFieldName].Summary[0].SummaryValue);
                    }
                }

                else if (e.GroupLevel >= 1)
                {
                    IntParentSummryRowHandle = view.GetParentRowHandle(view.GetParentRowHandle(e.RowHandle));
                    IntCurrentGroupRowHandle = view.GetParentRowHandle(e.RowHandle);

                    if (pStrFieldName == "RR_CARAT")
                    {
                        part = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        total = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        IntIndex = GetGroupSummryIndex("CONSUME_CARAT");
                        total += Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                    }
                    else if (pStrFieldName == "MAJOR_CARAT" || pStrFieldName == "MINOR_CARAT")
                    {
                        part = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        total = Val.Val(view.GetGroupSummaryValue(IntParentSummryRowHandle, (GridGroupSummaryItem)view.GroupSummary[GetGroupSummryIndex("CONSUME_CARAT")]));
                    }
                    else
                    {
                        part = Val.Val(view.GetGroupSummaryValue(IntCurrentGroupRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                        total = Val.Val(view.GetGroupSummaryValue(IntParentSummryRowHandle, (GridGroupSummaryItem)view.GroupSummary[IntIndex]));
                    }
                }
                e.TotalValue = (total == 0) ? 0 : (part / total) * 100;
            }
        }
        public void FooterSummary()
        {
            try
            {
                GridView1.PostEditor();
            }
            catch (Exception Ex)
            {
            }
        }
        private static List<string> ExtractFromString(string text, string startString, string endString)
        {
            List<string> matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            String FinalString = text;
            while (!exit)
            {
                indexStart = text.IndexOf(startString);
                indexEnd = text.IndexOf(endString);
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched.Add(text.Substring(indexStart + startString.Length,
                        indexEnd - indexStart - startString.Length));

                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                    exit = true;
            }
            return matched;
        }

        #endregion

        #region Grid Events
        private void GridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (MergeOnStr.Contains(e.Column.FieldName))
            {
                int val1 = Val.ToInt(GridView1.GetRowCellValue(e.RowHandle1, GridView1.Columns[MergeOn]));
                int val2 = Val.ToInt(GridView1.GetRowCellValue(e.RowHandle2, GridView1.Columns[MergeOn]));
                if (val1 == val2)
                    e.Merge = true;
                e.Handled = true;
            }
        }
        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                e.Column.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Excel;
            }
            catch (Exception ex)
            {
            }
        }
        private void GridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (Val.ToString(Remark).ToUpper().Equals("LEDGER_TRANSACTION"))
            {
                if (e.Column.FieldName.ToUpper().Contains("CREDIT") || e.Column.FieldName.ToUpper().Contains("DEBIT") || e.Column.FieldName.ToUpper().Contains("AMOUNT"))
                {
                    e.DisplayText = Val.FormatWithSeperator(e.DisplayText);
                }
            }
            else if (Val.ToString(Remark).ToUpper().Equals("POLISH_SALE_SUM_REPORT"))
            {
                if (e.Column.FieldName.ToUpper().Contains("DIFF_AMT") || e.Column.FieldName.ToUpper().Contains("REC_AMOUNT"))
                {
                    if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                    {
                        e.DisplayText = "0";
                    }
                }
                else
                {
                    if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                    {
                        e.DisplayText = String.Empty;

                    }
                }
            }
            else if (Val.ToString(Remark).ToUpper().Equals("MFG_JANGED_ISSUE_SUMMARY"))
            {
                if (e.Column.FieldName.ToUpper().Contains("CL_RATE") || e.Column.FieldName.ToUpper().Contains("CL_CARAT") || e.Column.FieldName.ToUpper().Contains("CL_AMOUNT"))
                {
                    if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                    {
                        e.DisplayText = "0";
                    }
                }
                else
                {
                    if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                    {
                        e.DisplayText = String.Empty;

                    }
                }
            }
            else if (Val.ToString(Remark).ToUpper().Equals("PARTY_ANALYSIS_REPORT"))
            {
                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                {
                    e.DisplayText = "0";
                }
            }
            else if (!Val.ToString(Remark).ToUpper().Equals("Party_Ledger"))
            {
                if (e.Column.FieldName.ToUpper().Contains("BALANCE_AMOUNT"))
                {
                    if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                    {
                        e.DisplayText = "0";
                    }
                }
                else
                {
                    if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                    {
                        e.DisplayText = String.Empty;

                    }
                }
            }
            else if (!Val.ToString(Remark).ToUpper().Equals("HR_SALARY_REPORT"))
            {
                if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
                {
                    e.DisplayText = String.Empty;

                }
            }
        }
        private void GridView1_StartGrouping(object sender, EventArgs e)
        {
            GridView1.BestFitColumns();
        }
        private void GridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            FooterSummary();
        }
        private void GridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (!Val.ToString(Remark).Trim().Equals(string.Empty))
            {
                if (Remark.ToUpper().Equals("EMP_DAILY_REPORT"))
                {
                    if (e.RowHandle >= 0)
                    {
                        for (int i = 0; i < GridView1.Columns.Count; i++)
                        {
                            GridView1.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
                        }
                    }
                }
                if (Remark.ToUpper().Equals("PAYMENT_PENDING_REMARK"))
                {
                    if (Val.ToString(GridView1.GetRowCellValue(e.RowHandle, "is_checked")) == "True")
                    {
                        e.Appearance.BeginUpdate();
                        e.Appearance.BackColor = Color.FromArgb(210, 170, 162);
                    }
                    //if (e.RowHandle >= 0)
                    //{
                    //    for (int i = 0; i < GridView1.Columns.Count; i++)
                    //    {
                    //        GridView1.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
                    //    }
                    //}
                }
                //if (Remark.ToUpper().Equals("CUT_DIFFRENCE_REPORT"))
                //{
                //    if (Val.ToString(GridView1.GetRowCellValue(e.RowHandle, "purity_name")) == "TOTAL")
                //    {
                //        e.Appearance.BeginUpdate();
                //        e.Appearance.BackColor = Color.FromArgb(210, 170, 162);
                //        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //    }
                //}
            }
        }
        private void GridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (Remark == null || Remark.ToString().Trim().Equals(string.Empty))
                return;
            DataRow DR = GridView1.GetDataRow(e.RowHandle);
            if (DR == null)
                return;
        }
        private void GridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData == false)
            {
                return;
            }

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "TOTAL_ROUGH_TRANSFER")
            {
                if (e.Column.FieldName == "RR_PER")
                {
                    e.Value = GetPercent(e.ListSourceRowIndex, "RR_CARAT");
                }

                if (e.Column.FieldName == "READY_PER")
                {
                    e.Value = GetPercent(e.ListSourceRowIndex, "READY_CARAT");
                }

                if (e.Column.FieldName == "READY_PER")
                {
                    e.Value = GetPercent(e.ListSourceRowIndex, "READY_CARAT");
                }
                if (e.Column.FieldName == "CONSUME_CARAT_PER")
                {
                    e.Value = GetPercent(e.ListSourceRowIndex, "CONSUME_CARAT");
                }
                if (e.Column.FieldName == "CONSUME_PCS_PER")
                {
                    e.Value = GetPercent(e.ListSourceRowIndex, "CONSUME_PCS");
                }
            }
        }
        private void GridView1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;

            #region Stock Ledger Detail Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "STOCK_LEDGER_DETAILS")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouOpeningCarat = 0;
                    DouOpeningAmount = 0;
                    DouConfirmCarat = 0;
                    DouConfirmAmount = 0;
                    DouSaleCarat = 0;
                    DouSaleAmount = 0;
                    DouRapCarat = 0;
                    DouRapAmount = 0;
                    DouOnHandCarat = 0;
                    DouOnHandAmount = 0;
                    DouRealHandCarat = 0;
                    DouRealHandAmount = 0;
                    DouWeightLossCarat = 0;
                    DouWeightLossAmount = 0;
                    DouWeightPlusCarat = 0;
                    DouWeightPlusAmount = 0;
                    DouLostCarat = 0;
                    DouLostAmount = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouOpeningCarat = DouOpeningCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_carat"));
                    DouOpeningAmount = DouOpeningAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_amount"));
                    DouConfirmCarat = DouConfirmCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "confirm_carat"));
                    DouConfirmAmount = DouConfirmAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "confirm_amount"));
                    DouSaleCarat = DouSaleCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sale_carat"));
                    DouSaleAmount = DouSaleAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sale_amount"));
                    DouRapCarat = DouRapCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rep_carat"));
                    DouRapAmount = DouRapAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rep_amount"));
                    DouOnHandCarat = DouOnHandCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "onhand_carat"));
                    DouOnHandAmount = DouOnHandAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "onhand_amount"));
                    DouRealHandCarat = DouRealHandCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "realhand_carat"));
                    DouRealHandAmount = DouRealHandAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "realhand_amount"));
                    DouWeightLossCarat = DouWeightLossCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "weight_loss_carat"));
                    DouWeightLossAmount = DouWeightLossAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "weight_loss_amount"));
                    DouWeightPlusCarat = DouWeightPlusCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "weight_plus_carat"));
                    DouWeightPlusAmount = DouWeightPlusAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "weight_plus_amount"));
                    DouLostCarat = DouLostCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lost_carat"));
                    DouLostAmount = DouLostAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lost_amount"));

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("opening_rate") == 0)
                    {
                        if (DouOpeningCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouOpeningAmount / DouOpeningCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("confirm_rate") == 0)
                    {
                        if (DouConfirmCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouConfirmAmount / DouConfirmCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("sale_rate") == 0)
                    {
                        if (DouSaleCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouSaleAmount / DouSaleCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("rep_rate") == 0)
                    {
                        if (DouRapCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouRapAmount / DouRapCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("onhand_rate") == 0)
                    {
                        if (DouOnHandCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouOnHandAmount / DouOnHandCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("realhand_rate") == 0)
                    {
                        if (DouRealHandCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouRealHandAmount / DouRealHandCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("weight_loss_rate") == 0)
                    {
                        if (DouWeightLossCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouWeightLossAmount / DouWeightLossCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("weight_plus_rate") == 0)
                    {
                        if (DouWeightPlusCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouWeightPlusAmount / DouWeightPlusCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("lost_rate") == 0)
                    {
                        if (DouLostCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouLostAmount / DouLostCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }

                }
            }
            #endregion

            #region Live Stock Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "LIVE_STOCK_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouClosingAmount = 0;
                    DouClosingCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouClosingCarat = DouClosingCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_carat"));
                    DouClosingAmount = DouClosingAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("cl_rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouClosingAmount / DouClosingCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region MFG Department Transfer Summary

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_DEPARTMENT_TRANSFER_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouClosingAmount = 0;
                    DouClosingCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouClosingCarat = DouClosingCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouClosingAmount = DouClosingAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouClosingAmount / DouClosingCarat, 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Inspection Pending Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "INSPECTION_PENDING_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouClosingAmount = 0;
                    DouClosingCarat = 0;
                    DouAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouClosingCarat = DouClosingCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "Iss_Carat"));
                    DouClosingAmount = DouClosingAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "Outstanding_Carat"));
                    DouAmount = DouAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "os_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouClosingAmount / DouClosingCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("os_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Stock Ledger In Out Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "STOCK_LEDGER_IN_OUT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouOpeningCarat = 0;
                    DouOpeningAmount = 0;
                    DouInCarat = 0;
                    DouInAmount = 0;
                    DouOutCarat = 0;
                    DouOutAmount = 0;
                    DouMixInCarat = 0;
                    DouMixInAmount = 0;
                    DouMixOutCarat = 0;
                    DouMixOutAmount = 0;
                    DouClosingAmount = 0;
                    DouClosingCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouOpeningCarat = DouOpeningCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_carat"));
                    DouOpeningAmount = DouOpeningAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_amount"));
                    DouInCarat = DouInCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "in_carat"));
                    DouInAmount = DouInAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "in_amount"));
                    DouOutCarat = DouOutCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "out_carat"));
                    DouOutAmount = DouOutAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "out_amount"));
                    DouMixInCarat = DouMixInCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_to_carat"));
                    DouMixInAmount = DouMixInAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_to_amount"));
                    DouMixOutCarat = DouMixOutCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_from_carat"));
                    DouMixOutAmount = DouMixOutAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_from_amount"));
                    DouClosingCarat = DouClosingCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "closing_carat"));
                    DouClosingAmount = DouClosingAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "closing_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("opening_rate") == 0)
                    {
                        if (DouOpeningCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouOpeningAmount / DouOpeningCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("in_rate") == 0)
                    {
                        if (DouInCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouInAmount / DouInCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("out_rate") == 0)
                    {
                        if (DouOutCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouOutAmount / DouOutCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("mix_to_rate") == 0)
                    {
                        if (DouMixInCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouMixInAmount / DouMixInCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("mix_from_rate") == 0)
                    {
                        if (DouMixOutCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouMixOutAmount / DouMixOutCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("closing_rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouClosingAmount / DouClosingCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Polish Sale Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "POLISH_SALE_SUM_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount"));
                    DouGrossAmount = DouGrossAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "gross_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("gross_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouGrossAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "POLISH_SALE_DET_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                    DouCurrentAmount = 0;

                    DouPurchaseAmount = 0;
                    DouSalesAmount = 0;
                    DouAmount = 0;
                    DouDiffAmount = 0;
                    DouPurchaseAmountTotal = 0;

                    num_Actual_Sale_Carat = 0;
                    num_Actual_Sale_Amount = 0;
                    num_Sale_Amount = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_sale_amount"));
                    DouCurrentAmount = DouCurrentAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "purchase_amount"));

                    num_Actual_Sale_Carat = num_Actual_Sale_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    num_Actual_Sale_Amount = num_Actual_Sale_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "actual_sale_amount"));
                    DouAmount = DouAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));

                    num_Sale_Amount = num_Sale_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sale_amount"));

                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "purchase_amount")) > 0)
                    {
                        DouPurchaseAmount = Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "purchase_amount"));
                        DouPurchaseAmountTotal = DouPurchaseAmountTotal + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "purchase_amount"));
                        DouSalesAmount = Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_sale_amount"));

                        DouDiffAmount = DouDiffAmount + Convert.ToDecimal(DouSalesAmount - DouPurchaseAmount);
                        if (DouDiffAmount != 0 && DouPurchaseAmountTotal != 0)
                        {
                            DouPL_Per = Math.Round(Convert.ToDecimal((DouDiffAmount / DouPurchaseAmountTotal) * 100), 2);
                        }
                        else
                        {
                            DouPL_Per = 0;
                        }
                    }

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("final_sale_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("purchase_rate") == 0)
                    {
                        if (DouTotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouCurrentAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_amt") == 0)
                    {
                        if (DouTotalAmount != 0)
                        {
                            decimal numDiff = Math.Round(DouTotalAmount - DouCurrentAmount);

                            e.TotalValue = Math.Round(Val.ToDecimal(numDiff / DouTotalAmount) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("profit_loss") == 0)
                    {
                        if (DouTotalAmount > 0)
                        {
                            e.TotalValue = Math.Round(DouPL_Per, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("actual_sale_rate") == 0)
                    {
                        if (num_Actual_Sale_Carat != 0)
                        {
                            e.TotalValue = Math.Round(num_Actual_Sale_Amount / num_Actual_Sale_Carat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(DouAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("sale_rate") == 0)
                    {
                        if (DouTotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(num_Sale_Amount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_rate") == 0)
                    {
                        if (DouTotalCarat != 0)
                        {
                            e.TotalValue = Math.Round(Math.Round(DouTotalAmount / DouTotalCarat, 2) - Math.Round(DouCurrentAmount / DouTotalCarat, 2), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Polish Purchase Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "POLISH_PURCHASE_SUM_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "POLISH_PURCHASE_DET_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Branch Transfer Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "BRANCH_TRANSFER_DET_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Branch Confirm Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "BRANCH_CONFIRM_DET_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region REJECTION TRANSFER FROM PURITY WISE DETAIL

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "REJECTION TRANSFER FROM PURITY WISE DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region MANUFACTURE LOTTING DETAIL

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MANUFACTURE LOTTING DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Demand Noting Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEMAND_NOTING_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Demand Noting Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEMAND_NOTING_RANGEWISE")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Memo Issue Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MEMO_ISSUE_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                    numTotal_Carat = 0;
                    numTotal_Amount = 0;
                    DouRejCarat = 0;
                    DouFinalAmount = 0;
                    num_CurrentAmount = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                    numTotal_Carat = numTotal_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "net_carat"));
                    numTotal_Amount = numTotal_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "net_amount"));
                    DouRejCarat = DouRejCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rejection_carat"));
                    DouFinalAmount = DouFinalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat")) * Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_rate"));
                    num_CurrentAmount = num_CurrentAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat")) * Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "price_rate"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("net_rate") == 0)
                    {
                        if (numTotal_Carat > 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount / numTotal_Carat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("rejection_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouRejCarat * 100) / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("final_rate") == 0)
                    {
                        //DouAvgDays = Math.Round(Val.ToDouble(GridView1.Columns["final_term_days"].SummaryText), 0);
                        //DouSaleRate = Val.ToDouble(Math.Round(numTotal_Amount / numTotal_Carat, 2));
                        //DouRejPer = Val.ToDouble(Math.Round((DouRejCarat * 100) / DouTotalCarat, 2));
                        //DouFinalRate = Math.Round(DouSaleRate - (DouSaleRate * ((DouAvgDays * 0.04) / 100)) - (DouSaleRate * ((DouRejPer * 0.05) / 100)), 2);
                        //if (DouFinalRate > 0)
                        //{
                        //    e.TotalValue = Math.Round(DouFinalRate, 2);
                        //}
                        //else
                        //{
                        //    e.TotalValue = 0;
                        //}

                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouFinalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }

                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("price_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(num_CurrentAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }

                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouFinalAmount / DouTotalCarat, 2) - Math.Round(num_CurrentAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }

                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(((Math.Round(DouFinalAmount / DouTotalCarat, 2) - Math.Round(num_CurrentAmount / DouTotalCarat, 2)) / Math.Round(num_CurrentAmount / DouTotalCarat, 2)) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Memo Receive Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MEMO_RECEIVE_SUM_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                    DouLostCarat = 0;
                    DouLostAmount = 0;
                    DouRejCarat = 0;
                    DouRejAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                    DouLostCarat = DouLostCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lost_carat"));
                    DouLostAmount = DouLostAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lost_amount"));
                    DouLossCarat = DouLossCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "loss_carat"));
                    DouLossAmount = DouLossAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "loss_amount"));
                    DouRejCarat = DouRejCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rej_carat"));
                    DouRejAmount = DouRejAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rej_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("lost_rate") == 0)
                    {
                        if (DouLostCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouLostAmount / DouLostCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("loss_rate") == 0)
                    {
                        if (DouLossCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouLossAmount / DouLossCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("rej_rate") == 0)
                    {
                        if (DouRejCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouRejAmount / DouRejCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Rough Purchase Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "PURCHASE_MFG_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Kapan MFG Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_KAPAN_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Rough Cut Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_ROUGHCUT_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Rough Stock Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_ROUGHSTK_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "balance_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Process Issue Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_PROCESS_ISSUE_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Kapan Mix Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_KAPAN_MIX_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Process Receive Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_PROCESS_RECEIVE_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Assortment Process Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "ASSORTMENT_PROCESS_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Mix Split Report
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MIX_SPLIT_REPORT_FORMAT1")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  MFG Stock Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_STOCK_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                    DouPrdAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "balance_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                    DouPrdAmount = DouPrdAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "prd_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("prd_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouPrdAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Mix Split Report Detail
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MIX_SPLIT_REPORT_DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                    DouToTotalCarat = 0;
                    DouToTotalAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "from_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "from_amount"));
                    DouToTotalCarat = DouToTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "to_carat"));
                    DouToTotalAmount = DouToTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "to_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("from_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("to_rate") == 0)
                    {
                        if (DouToTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouToTotalAmount / DouToTotalCarat, 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_rate") == 0)
                    {
                        if (DouToTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(Val.ToDecimal(DouToTotalAmount - DouTotalAmount) / DouToTotalCarat, 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_per") == 0)
                    {
                        if (DouToTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((Val.ToDecimal(DouToTotalAmount - DouTotalAmount) / DouTotalAmount) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region "Credit Debit Report"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "CREDIT_DEBIT_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {

                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "Credit_Amt")) - Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "Debit_Amt"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("Balance") == 0)
                    {
                        if (DouTotalAmount > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region "MFG Janged Issue Summary"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_JANGED_ISSUE_SUMMARY")
            {

                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    numTotal_Carat = 0;
                    numTotal_Amount = 0;
                    num_Total_Carat_2 = 0;
                    numTotal_Amount_2 = 0;
                    num_Total_Carat_3 = 0;
                    numTotal_Amount_3 = 0;
                    num_Total_Carat_4 = 0;
                    numTotal_Amount_4 = 0;
                    num_Total_Carat_5 = 0;
                    numTotal_Amount_5 = 0;
                    num_Total_Carat_6 = 0;
                    numTotal_Amount_6 = 0;
                    num_Total_Carat_7 = 0;
                    numTotal_Amount_7 = 0;
                    num_Total_Carat_9 = 0;
                    numTotal_Amount_9 = 0;
                    num_Total_Carat_10 = 0;
                    numTotal_Amount_10 = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_carat")) > 0)
                    {
                        numTotal_Carat = numTotal_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_carat"));
                        numTotal_Amount = numTotal_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_amount"));
                    }

                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "inward_carat")) > 0)
                    {
                        num_Total_Carat_2 = num_Total_Carat_2 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "inward_carat"));
                        numTotal_Amount_2 = numTotal_Amount_2 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "inward_amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "trf_carat")) > 0)
                    {
                        num_Total_Carat_3 = num_Total_Carat_3 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "trf_carat"));
                        numTotal_Amount_3 = numTotal_Amount_3 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "trf_amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lotting_rej_carat")) > 0)
                    {
                        num_Total_Carat_4 = num_Total_Carat_4 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lotting_rej_carat"));
                        numTotal_Amount_4 = numTotal_Amount_4 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "lotting_rej_amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat")) > 0)
                    {
                        num_Total_Carat_5 = num_Total_Carat_5 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat"));
                        numTotal_Amount_5 = numTotal_Amount_5 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat")) > 0)
                    {
                        num_Total_Carat_6 = num_Total_Carat_6 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat"));
                        numTotal_Amount_6 = numTotal_Amount_6 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_in_carat")) > 0)
                    {
                        num_Total_Carat_7 = num_Total_Carat_7 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_in_carat"));
                        numTotal_Amount_7 = numTotal_Amount_7 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_in_amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_carat")) > 0)
                    {
                        num_Total_Carat_9 = num_Total_Carat_9 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_carat"));
                        numTotal_Amount_9 = numTotal_Amount_9 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_amount"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_in_carat")) > 0)
                    {
                        num_Total_Carat_10 = num_Total_Carat_10 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_in_carat"));
                        numTotal_Amount_10 = numTotal_Amount_10 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "mix_in_amount"));
                    }
                }

                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("opening_rate") == 0)
                    {
                        if (numTotal_Carat != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount / numTotal_Carat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("inward_rate") == 0)
                    {
                        if (num_Total_Carat_2 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_2 / num_Total_Carat_2, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("trf_rate") == 0)
                    {
                        if (num_Total_Carat_3 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_3 / num_Total_Carat_3, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("lotting_rej_rate") == 0)
                    {
                        if (num_Total_Carat_4 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_4 / num_Total_Carat_4, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("polish_rate") == 0)
                    {
                        if (num_Total_Carat_5 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_5 / num_Total_Carat_5, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (num_Total_Carat_6 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_6 / num_Total_Carat_6, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("cl_rate") == 0)
                    {
                        if (num_Total_Carat_9 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_9 / num_Total_Carat_9, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("mix_in_rate") == 0)
                    {
                        if (num_Total_Carat_10 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_10 / num_Total_Carat_10, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region "LIVE STOCK WITH REJECTION"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "RPT_LIVE_STOCK_WITH_REJECTION")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    numTotal_Carat = 0;
                    numTotal_Amount = 0;

                    num_Total_Carat_2 = 0;
                    numTotal_Amount_2 = 0;

                    num_Total_Carat2 = 0;
                    numTotal_Amount2 = 0;

                    num_Total_Carat00 = 0;
                    numTotal_Amount00 = 0;

                    num_Final_Total_Carat = 0;
                    num_Final_Total_Amount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat")) > 0)
                    {
                        numTotal_Carat = numTotal_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat"));
                        numTotal_Amount = numTotal_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount"));
                    }

                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_-2")) > 0)
                    {
                        num_Total_Carat_2 = num_Total_Carat_2 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_-2"));
                        numTotal_Amount_2 = numTotal_Amount_2 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount_-2"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_+2")) > 0)
                    {
                        num_Total_Carat2 = num_Total_Carat2 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_+2"));
                        numTotal_Amount2 = numTotal_Amount2 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount_+2"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_00")) > 0)
                    {
                        num_Total_Carat00 = num_Total_Carat00 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_00"));
                        numTotal_Amount00 = numTotal_Amount00 + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount_00"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_total_carat")) > 0)
                    {
                        num_Final_Total_Carat = num_Final_Total_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_total_carat"));
                        num_Final_Total_Amount = num_Final_Total_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_total_amount"));
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate") == 0)
                    {
                        if (numTotal_Carat != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount / numTotal_Carat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate_-2") == 0)
                    {
                        if (num_Total_Carat_2 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount_2 / num_Total_Carat_2, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate_+2") == 0)
                    {
                        if (num_Total_Carat2 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount2 / num_Total_Carat2, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate_00") == 0)
                    {
                        if (num_Total_Carat00 != 0)
                        {
                            e.TotalValue = Math.Round(numTotal_Amount00 / num_Total_Carat00, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("final_total_rate") == 0)
                    {
                        if (num_Final_Total_Carat != 0)
                        {
                            e.TotalValue = Math.Round(num_Final_Total_Amount / num_Final_Total_Carat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Process Issue Receive Pending Summary Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_PROCESS_ISSUEREC_PENDING_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region "Aging Reports"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "AGING_REPORTS")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    numTotal_Carat = 0;

                    num_Final_Total_Carat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "aging_carat")) > 0)
                    {
                        numTotal_Carat = numTotal_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "aging_carat"));
                        num_Final_Total_Carat = num_Final_Total_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "final_carat"));
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("aging_days") == 0)
                    {
                        if (numTotal_Carat != 0)
                        {
                            e.TotalValue = Math.Round(num_Final_Total_Carat / numTotal_Carat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region "Factory Janged Detail Reports"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "FACTORY_JANGED_DETAILS")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    num_Org_Carat = 0;
                    num_Saring_Carat = 0;
                    num_4p_Carat = 0;
                    num_Polish_Carat = 0;
                    num_Russion_Carat = 0;
                    num_K_Carat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    num_Org_Carat = num_Org_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "org_carat"));
                    num_K_Carat = num_K_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_wt"));

                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sarin_carat")) > 0)
                    {
                        num_Saring_Carat = num_Saring_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sarin_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "ok_carat")) > 0)
                    {
                        num_4p_Carat = num_4p_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "ok_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat")) > 0)
                    {
                        num_Polish_Carat = num_Polish_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat")) > 0)
                    {
                        num_Russion_Carat = num_Russion_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat"));
                    }
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("sarin_per") == 0)
                    {
                        if (num_Org_Carat != 0)
                        {
                            e.TotalValue = Math.Round((num_Saring_Carat / num_Org_Carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("ok_per") == 0)
                    {
                        if (num_Org_Carat != 0)
                        {
                            e.TotalValue = Math.Round((num_4p_Carat / num_K_Carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("polish_per") == 0)
                    {
                        if (num_Org_Carat != 0)
                        {
                            e.TotalValue = Math.Round((num_Polish_Carat / num_Org_Carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rus_per") == 0)
                    {
                        if (num_Org_Carat != 0)
                        {
                            e.TotalValue = Math.Round((num_Russion_Carat / num_Org_Carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_per") == 0)
                    {
                        if (num_Org_Carat != 0)
                        {
                            e.TotalValue = Math.Round((num_Saring_Carat / num_Org_Carat) * 100, 2) - Math.Round((num_Russion_Carat / num_K_Carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region "Janged Receive Reports"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_JANGED_RECIEVE_SUMMARY")
            {
                if (ReportParams_Property.Department_Type == "4P")
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {
                        num_issue_carat = 0;
                        num_receive_carat = 0;
                        num_k_carat = 0;
                        num_k_pcs = 0;
                        num_receive_pcs = 0;
                        num_polish_carat = 0;
                        num_sarin_carat = 0;
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat")) > 0)
                        {
                            num_issue_carat = num_issue_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat")) > 0)
                        {
                            num_receive_carat = num_receive_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat")) > 0)
                        {
                            num_k_carat = num_k_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_pcs")) > 0)
                        {
                            num_k_pcs = num_k_pcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_pcs"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_pcs")) > 0)
                        {
                            num_receive_pcs = num_receive_pcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_pcs"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sarin_carat")) > 0)
                        {
                            num_sarin_carat = num_sarin_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sarin_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat")) > 0)
                        {
                            num_polish_carat = num_polish_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat"));
                        }
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo("russion_per") == 0)
                        {
                            if (num_issue_carat != 0)
                            {
                                e.TotalValue = Math.Round((num_sarin_carat / num_issue_carat) * 100, 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }


                        if (((GridSummaryItem)e.Item).FieldName.CompareTo("russionrk_per") == 0)
                        {
                            if (num_receive_carat != 0)
                            {
                                e.TotalValue = Math.Round((num_receive_carat / num_k_carat) * 100, 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }

                        if (((GridSummaryItem)e.Item).FieldName.CompareTo("fourp_per") == 0)
                        {
                            if (num_k_carat != 0)
                            {
                                e.TotalValue = Math.Round((num_polish_carat / num_k_carat) * 100, 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo("k_per") == 0)
                        {
                            if (num_receive_pcs != 0)
                            {
                                e.TotalValue = Math.Round((num_k_pcs / num_receive_pcs) * 100, 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }
                }
                else
                {
                    if (e.SummaryProcess == CustomSummaryProcess.Start)
                    {
                        num_issue_carat = 0;
                        num_receive_carat = 0;
                        num_k_carat = 0;
                        num_k_pcs = 0;
                        num_receive_pcs = 0;
                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                    {
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat")) > 0)
                        {
                            num_issue_carat = num_issue_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat")) > 0)
                        {
                            num_receive_carat = num_receive_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat")) > 0)
                        {
                            num_k_carat = num_k_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_pcs")) > 0)
                        {
                            num_k_pcs = num_k_pcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_pcs"));
                        }
                        if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_pcs")) > 0)
                        {
                            num_receive_pcs = num_receive_pcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_pcs"));
                        }

                    }
                    else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                    {
                        if (((GridSummaryItem)e.Item).FieldName.CompareTo("russion_per") == 0)
                        {
                            if (num_issue_carat != 0)
                            {
                                e.TotalValue = Math.Round((num_receive_carat / num_issue_carat) * 100, 2);
                            }
                            else
                            {
                                e.TotalValue = 0;
                            }
                        }
                    }

                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("russionrk_per") == 0)
                    {
                        if (num_receive_carat != 0)
                        {
                            e.TotalValue = Math.Round((num_receive_carat / num_k_carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }

                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("fourp_per") == 0)
                    {
                        if (num_receive_carat != 0)
                        {
                            e.TotalValue = Math.Round((num_issue_carat / num_k_carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("k_per") == 0)
                    {
                        if (num_receive_pcs != 0)
                        {
                            e.TotalValue = Math.Round((num_k_pcs / num_receive_pcs) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "AANGADIYA_INSURANCE_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    num_issue_carat = 0;
                    numTotal_Amount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat")) > 0)
                    {
                        num_issue_carat = num_issue_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount")) > 0)
                    {
                        numTotal_Amount = numTotal_Amount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                    }

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (num_issue_carat != 0)
                        {
                            e.TotalValue = Math.Round((numTotal_Amount / num_issue_carat), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }

                }
            }
            #endregion

            #region "Patta Ok (%)"

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_PATTA_OK")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    num_issue_carat = 0;
                    num_receive_carat = 0;
                    num_k_carat = 0;
                    num_k_pcs = 0;
                    num_receive_pcs = 0;
                    num_table_carat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat")) > 0)
                    {
                        num_issue_carat = num_issue_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "issue_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "table_carat")) > 0)
                    {
                        num_table_carat = num_table_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "table_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat")) > 0)
                    {
                        num_receive_carat = num_receive_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat")) > 0)
                    {
                        num_k_carat = num_k_carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_pcs")) > 0)
                    {
                        num_k_pcs = num_k_pcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_pcs"));
                    }
                    if (Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_pcs")) > 0)
                    {
                        num_receive_pcs = num_receive_pcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "receive_pcs"));
                    }

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("russion_per") == 0)
                    {
                        if (num_issue_carat != 0)
                        {
                            e.TotalValue = Math.Round((num_receive_carat / num_issue_carat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }

                if (((GridSummaryItem)e.Item).FieldName.CompareTo("russionrk_per") == 0)
                {
                    if (num_receive_carat != 0)
                    {
                        e.TotalValue = Math.Round((num_receive_carat / num_k_carat) * 100, 2);
                    }
                    else
                    {
                        e.TotalValue = 0;
                    }
                }

                if (((GridSummaryItem)e.Item).FieldName.CompareTo("fourp_per") == 0)
                {
                    if (num_receive_carat != 0)
                    {
                        e.TotalValue = Math.Round((num_issue_carat / num_k_carat) * 100, 2);
                    }
                    else
                    {
                        e.TotalValue = 0;
                    }
                }
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("k_per") == 0)
                {
                    if (num_receive_pcs != 0)
                    {
                        e.TotalValue = Math.Round((num_k_pcs / num_receive_pcs) * 100, 2);
                    }
                    else
                    {
                        e.TotalValue = 0;
                    }
                }
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("table_per") == 0)
                {
                    if (num_issue_carat != 0)
                    {
                        e.TotalValue = Math.Round((num_table_carat / num_issue_carat) * 100, 2);
                    }
                    else
                    {
                        e.TotalValue = 0;
                    }
                }
                if (((GridSummaryItem)e.Item).FieldName.CompareTo("patta_ok_per") == 0)
                {
                    if (num_table_carat != 0)
                    {
                        e.TotalValue = Math.Round((num_receive_carat / num_table_carat) * 100, 2);
                    }
                    else
                    {
                        e.TotalValue = 0;
                    }
                }
            }
            #endregion

            #region  Costing Manual Entry

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "COSTING_MANUAL_ENTRY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouPolishCarat = 0;
                    DouKapanCarat = 0;
                    DouDiffPcs = 0;
                    DouKapanPcs = 0;
                    DouPolishPcs = 0;
                    DouCostingAmtPer = 0;
                    DouCostingRate = 0;
                    DouCostingAvg = 0;
                    DouDifference = 0;
                    DouTotalLabourAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouKapanCarat = DouKapanCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "kapan_carat"));
                    DouPolishCarat = DouPolishCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat"));
                    DouDiffPcs = DouDiffPcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "diff_pcs"));
                    DouKapanPcs = DouKapanPcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "kapan_pcs"));
                    DouPolishPcs = DouPolishPcs + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_pcs"));
                    DouDifference = DouDifference + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "difference"));
                    DouCostingAmtPer = DouCostingAmtPer + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "costing_amt_per"));
                    DouCostingRate = DouCostingRate + Math.Round((Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "kapan_carat")) * Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rate"))), 2);
                    DouCostingAvg = DouCostingAvg + Math.Round((Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "polish_carat")) * Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "average"))), 2);
                    DouTotalLabourAmount = DouTotalLabourAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "labour_amt"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("polish_per") == 0)
                    {
                        if (DouKapanCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouPolishCarat / DouKapanCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("diff_per") == 0)
                    {
                        if (DouKapanPcs > 0)
                        {
                            e.TotalValue = Math.Round((DouDiffPcs / DouKapanPcs) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("costing") == 0)
                    {
                        if (DouPolishCarat > 0)
                        {
                            //e.TotalValue = Math.Round((DouCostingAmtPer / DouPolishCarat) / 100, 2);
                            e.TotalValue = Math.Round((DouCostingAmtPer / DouPolishCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouKapanCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouCostingRate / DouKapanCarat), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("average") == 0)
                    {
                        if (DouPolishCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouCostingAvg / DouPolishCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("difference") == 0)
                    {
                        if (DouPolishCarat > 0)
                        {
                            //e.TotalValue = Math.Round(((Math.Round((DouCostingAvg / DouPolishCarat), 2)) - (Math.Round((DouCostingAmtPer / DouPolishCarat) / 100, 2))), 2);
                            e.TotalValue = Math.Round(((Math.Round((DouCostingAvg / DouPolishCarat), 2)) - (Math.Round((DouCostingAmtPer / DouPolishCarat), 2))), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("k_size") == 0)
                    {
                        if (DouKapanCarat > 0)
                        {
                            //e.TotalValue = Math.Round(((Math.Round((DouCostingAvg / DouPolishCarat), 2)) - (Math.Round((DouCostingAmtPer / DouPolishCarat) / 100, 2))), 2);
                            e.TotalValue = Math.Round((DouKapanPcs / DouKapanCarat), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("polish_size") == 0)
                    {
                        if (DouKapanCarat > 0)
                        {
                            //e.TotalValue = Math.Round(((Math.Round((DouCostingAvg / DouPolishCarat), 2)) - (Math.Round((DouCostingAmtPer / DouPolishCarat) / 100, 2))), 2);
                            e.TotalValue = Math.Round((DouPolishPcs / DouPolishCarat), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("difference_per") == 0)
                    {
                        if (DouCostingAvg > 0)
                        {
                            e.TotalValue = Math.Round(((Math.Round((DouCostingAvg / DouPolishCarat), 2)) - (Math.Round((DouCostingAmtPer / DouPolishCarat), 2))) / Math.Round((DouCostingAmtPer / DouPolishCarat), 2) * 100, 2);
                            //e.TotalValue = Math.Round(((Math.Round((DouCostingAvg / DouPolishCarat), 2)) - (Math.Round((DouCostingAmtPer / DouPolishCarat), 2))) / Math.Round((DouCostingAvg / DouPolishCarat), 2) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("labour_rate") == 0)
                    {
                        if (DouPolishPcs > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalLabourAmount / DouPolishPcs), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Rough Sale Stock Closing Rate

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_ROUGH_SALE_STOCK_CLOSING_RATE")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouOpeningCarat = 0;
                    DouOpeningAmount = 0;
                    DouInCarat = 0;
                    DouInAmount = 0;
                    DouTotalCarat = 0;
                    DouTotalAmount = 0;
                    DouSaleCarat = 0;
                    DouSaleAmount = 0;
                    DouOnHandCarat = 0;
                    DouOnHandAmount = 0;
                    DouClosingCarat = 0;
                    DouClosingAmount = 0;
                    DouJangedCarat = 0;
                    DouJangedAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouOpeningCarat = DouOpeningCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_carat"));
                    DouOpeningAmount = DouOpeningAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "opening_amount"));
                    DouInCarat = DouInCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "in_carat"));
                    DouInAmount = DouInAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "in_amount"));
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_amount"));
                    DouSaleCarat = DouSaleCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sale_carat"));
                    DouSaleAmount = DouSaleAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "sale_amount"));
                    DouOnHandCarat = DouOnHandCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "on_hand_carat"));
                    DouOnHandAmount = DouOnHandAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "on_hand_amount"));
                    DouClosingCarat = DouClosingCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_carat"));
                    DouClosingAmount = DouClosingAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "cl_amount"));
                    DouJangedCarat = DouJangedCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "janged_carat"));
                    DouJangedAmount = DouJangedAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "janged_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("opening_rate") == 0)
                    {
                        if (DouOpeningCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouOpeningAmount / DouOpeningCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("in_rate") == 0)
                    {
                        if (DouInCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouInAmount / DouInCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("total_rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalAmount / DouTotalCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("sale_rate") == 0)
                    {
                        if (DouSaleCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouSaleAmount / DouSaleCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("cl_rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouClosingAmount / DouClosingCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("on_hand_rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouOnHandAmount / DouClosingCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("janged_rate") == 0)
                    {
                        if (DouJangedCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouJangedAmount / DouJangedCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region REJECTION DETAIL

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "REJECTION DETAIL" || Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "REJECTION KAPAN WISE PURITY SUMMARY"
                || Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "REJECTION KAPAN WISE PURITY DETAIL" || Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "REJECTION PURITY WISE SUMMARY"
                || Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "ROUGH SALE SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "ROUGH SALE DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "gross_amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Lotting Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "RPT_MFG_LOTTING_COMPLETED")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalAmount = 0;
                    DouTotalCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "k_carat"));
                    DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Cut Commparision Report

            //if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "CUT_DIFFRENCE_REPORT")
            //{
            //    if (e.SummaryProcess == CustomSummaryProcess.Start)
            //    {
            //        DouTotalAmount = 0;
            //        DouTotalCarat = 0;
            //    }
            //    else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            //    {
            //        DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "M15-1611-11_-2WT"));
            //        DouTotalCarat = Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "M15-1611-11_-2WT"));
            //        //DouTotalAmount = DouTotalAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
            //    }
            //    else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            //    {
            //        if (((GridSummaryItem)e.Item).FieldName.CompareTo("M15-1611-11_minus_two_per") == 0)
            //        {
            //            if (DouTotalCarat > 0)
            //            {
            //                e.TotalValue = Math.Round(DouTotalAmount / DouTotalCarat, 2);
            //            }
            //            else
            //            {
            //                e.TotalValue = 0;
            //            }
            //        }
            //    }
            //}
            #endregion

            #region Cut Commparision Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "PURITY_CLARITY_WISE_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouTotalCarat = 0;
                    DouTotalOrnCarat = 0;
                    DouTotalJweCarat = 0;
                    DouTotalDlxCarat = 0;
                    DouTotalFineCarat = 0;
                    DouTotalKhadCarat = 0;
                    DouTotalBrakCarat = 0;

                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouTotalCarat = DouTotalCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat"));

                    DouTotalOrnCarat = DouTotalOrnCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "orn"));
                    DouTotalJweCarat = DouTotalJweCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "jwe"));
                    DouTotalDlxCarat = DouTotalDlxCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "dlx"));
                    DouTotalFineCarat = DouTotalFineCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "fine"));
                    DouTotalKhadCarat = DouTotalKhadCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "khad"));
                    DouTotalBrakCarat = DouTotalBrakCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "brak"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("orn_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalOrnCarat / DouTotalCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("jwe_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalJweCarat / DouTotalCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("dlx_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalDlxCarat / DouTotalCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("fine_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalFineCarat / DouTotalCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("khad_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalKhadCarat / DouTotalCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    else if (((GridSummaryItem)e.Item).FieldName.CompareTo("brak_per") == 0)
                    {
                        if (DouTotalCarat > 0)
                        {
                            e.TotalValue = Math.Round((DouTotalBrakCarat / DouTotalCarat) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region Live Stock Report MFG

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MFG_LIVE_STOCK_SUMMARY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouClosingAmount = 0;
                    DouClosingCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouClosingCarat = DouClosingCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "balance_carat"));
                    DouClosingAmount = DouClosingAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouClosingCarat > 0)
                        {
                            e.TotalValue = Math.Round(DouClosingAmount / DouClosingCarat, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Galaxy Lot Wise Report

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "GALAXY_REPORT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouP_Carat = 0;
                    DouRough_Wt = 0;
                    DouSaw_Wt = 0;
                    makable_pcs = 0;
                    Total_Polish_Dollar = 0;
                    lot_no = 0;
                    P_Carat = 0;
                    est_wt = 0;
                    Total_Dollar = 0;
                    DouCarat = 0;
                    total_carat_dollar = 0;
                    xyz = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouP_Carat = DouP_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "exp_carat"));
                    DouRough_Wt = DouRough_Wt + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "makable_carat"));
                    DouSaw_Wt = DouSaw_Wt + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "rough_carat"));

                    makable_pcs = makable_pcs + Val.ToInt64(GridView1.GetRowCellValue(e.RowHandle, "makable_pcs"));
                    lot_no = lot_no + Val.ToInt64(GridView1.GetRowCellValue(e.RowHandle, "lot_no"));
                    DouCarat = DouCarat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "carat"));
                    total_carat_dollar = total_carat_dollar + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_carat_dollar"));

                    Total_Polish_Dollar = Total_Polish_Dollar + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_pol_dollar"));

                    P_Carat = P_Carat + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "p_carat"));
                    est_wt = est_wt + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "est_rwt"));
                    Total_Dollar = Total_Dollar + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "total_dollar"));
                    xyz = xyz + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "xyz"));

                    //Lot_No = Lot_No + "," + Val.ToInt(GridView1.GetRowCellValue(e.RowHandle, "lotno"));
                    //Lot_No_New = Val.ToString(GridView1.GetRowCellValue(e.RowHandle, "lotno"));

                    ////DouCountLot = DouCountLot + 1;
                    //if (Lot_No.ToString().Contains(Lot_No_New))
                    //{
                    //    DouCountLot = DouCountLot + 1;
                    //    if (!Lot_No.ToString().Contains(Lot_No_New))
                    //    {
                    //        DouCountLot = DouCountLot + 1;
                    //    }
                    //}
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("exp_per") == 0)
                    {
                        if (DouRough_Wt > 0)
                        {
                            e.TotalValue = Math.Round((DouP_Carat / DouRough_Wt) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("ro_pol_per") == 0)
                    {
                        if (DouRough_Wt > 0)
                        {
                            e.TotalValue = Math.Round((DouP_Carat / DouSaw_Wt) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rough_size") == 0)
                    {
                        if (DouSaw_Wt > 0)
                        {
                            e.TotalValue = Math.Round((makable_pcs / DouSaw_Wt), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("craft_size") == 0)
                    {
                        if (DouRough_Wt > 0)
                        {
                            e.TotalValue = Math.Round((makable_pcs / DouRough_Wt), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("craft_size_new") == 0)
                    {
                        if (DouCarat > 0)
                        {
                            e.TotalValue = Math.Round((lot_no / DouCarat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rough_to_pol") == 0)
                    {
                        if (est_wt > 0)
                        {
                            e.TotalValue = Math.Round((P_Carat / est_wt) * 100, 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("ro_dolar_carat") == 0)
                    {
                        if (DouCarat > 0 || est_wt > 0)
                        {
                            // e.TotalValue = Math.Round(((total_carat_dollar / (est_wt)) - ((lot_no / DouCarat))) * 75, 0);
                            e.TotalValue = Math.Round(((total_carat_dollar / est_wt) - xyz), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("po_per_carat") == 0)
                    {
                        if (P_Carat > 0)
                        {
                            e.TotalValue = Math.Round(total_carat_dollar / P_Carat, 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("polish_size") == 0)
                    {
                        if (DouP_Carat > 0)
                        {
                            e.TotalValue = Math.Round((makable_pcs / DouP_Carat), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rough_dollar_per_cts") == 0)
                    {
                        if (DouSaw_Wt > 0)
                        {
                            e.TotalValue = Math.Round((Total_Polish_Dollar / DouSaw_Wt), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("polish_dollar_per_cts") == 0)
                    {
                        if (DouP_Carat > 0)
                        {
                            e.TotalValue = Math.Round((Total_Polish_Dollar / DouP_Carat), 0);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion
        }
        private void GridView1_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            var lvl = e.Level;
            switch (lvl)
            {
                case 0:
                    SetLvlBackColor(e, Color.FromArgb(163, 196, 188));
                    break;
                case 1:
                    SetLvlBackColor(e, Color.FromArgb(98, 146, 158));
                    break;
                case 2:
                    SetLvlBackColor(e, Color.FromArgb(88, 123, 127));
                    break;
                case 3:
                    SetLvlBackColor(e, Color.FromArgb(244, 247, 245));
                    break;
                case 4:
                    SetLvlBackColor(e, Color.FromArgb(207, 232, 239));
                    break;
                case 5:
                    SetLvlBackColor(e, Color.FromArgb(198, 224, 180));
                    break;
                case 6:
                    SetLvlBackColor(e, Color.FromArgb(248, 203, 173));
                    break;
                case 7:
                    SetLvlBackColor(e, Color.FromArgb(199, 157, 245));
                    break;
                case 8:
                    SetLvlBackColor(e, Color.FromArgb(150, 174, 252));
                    break;
                case 9:
                    SetLvlBackColor(e, Color.FromArgb(192, 181, 221));
                    break;
                case 10:
                    SetLvlBackColor(e, Color.FromArgb(225, 231, 171));
                    break;
            }
        }
        private void GridView1_PrintInitialize(object sender, PrintInitializeEventArgs e)
        {
            PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
            pb.PageSettings.Landscape = true;
            pb.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A3;
        }
        private void GridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "RPT_LIVE_STOCK_WITH_REJECTION")
                {
                    if (e.Column.FieldName == "assort_name" ||
                    e.Column.FieldName == "color_name" ||
                    e.Column.FieldName == "-2" ||
                    e.Column.FieldName == "Rej-2" ||
                    e.Column.FieldName == "+2" ||
                    e.Column.FieldName == "Rej+2" ||
                    e.Column.FieldName == "-00" ||
                    e.Column.FieldName == "Rej-00"
                    )
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                    }

                    if (e.Column.FieldName == "total_carat" ||
                        e.Column.FieldName == "total_rate" ||
                        e.Column.FieldName == "total_amount")
                    {
                        e.Appearance.BackColor = Color.LightGoldenrodYellow;
                    }
                    if (e.Column.FieldName == "assort_name_new" ||
                        e.Column.FieldName == "total_carat_-2" ||
                        e.Column.FieldName == "total_rate_-2" ||
                        e.Column.FieldName == "total_amount_-2" ||
                        e.Column.FieldName == "total_carat_+2" ||
                        e.Column.FieldName == "total_rate_+2" ||
                        e.Column.FieldName == "total_amount_+2" ||
                        e.Column.FieldName == "total_carat_00" ||
                        e.Column.FieldName == "total_rate_00" ||
                        e.Column.FieldName == "total_amount_00"

                        )
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 230, 230);
                    }
                    if (e.Column.FieldName == "final_total_carat" ||
                        e.Column.FieldName == "final_total_rate" ||
                        e.Column.FieldName == "final_total_amount")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 230, 240);
                    }

                    if (e.Column.FieldName == "color_shortname")
                    {
                        e.Appearance.BackColor = Color.FromArgb(230, 255, 255);
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    if (e.Column.FieldName == "-2" || e.Column.FieldName == "Rej-2" || e.Column.FieldName == "+2" || e.Column.FieldName == "Rej+2" || e.Column.FieldName == "-00" || e.Column.FieldName == "Rej-00" || e.Column.FieldName == "total_carat" ||
                        e.Column.FieldName == "total_carat_-2" || e.Column.FieldName == "total_carat_+2" || e.Column.FieldName == "total_carat_00" || e.Column.FieldName == "final_total_carat"
                        )
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }

                    if (e.Column.FieldName == "aging_days_-2" ||
                       e.Column.FieldName == "aging_days_+2" ||
                       e.Column.FieldName == "aging_days_00" ||
                       e.Column.FieldName == "total_days")
                    {
                        e.Appearance.BackColor = Color.FromArgb(252, 186, 186);
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
                if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "MEMO_ISSUE_SUMMARY")
                {
                    if (sender is GridView)
                    {
                        GridView gView = (GridView)sender;
                        if (!gView.IsValidRowHandle(e.RowHandle)) return;
                        int parent = gView.GetParentRowHandle(e.RowHandle);
                        if (gView.IsGroupRow(parent))
                        {
                            for (int i = 0; i < gView.GetChildRowCount(parent); i++)
                            {
                                if (gView.GetChildRowHandle(parent, i) == e.RowHandle)
                                {
                                    e.Appearance.BackColor = i % 2 == 0 ? Color.AliceBlue : Color.FromArgb(215, 201, 207);
                                }
                            }
                        }
                        else
                        {
                            e.Appearance.BackColor = e.RowHandle % 2 == 0 ? Color.AliceBlue : Color.FromArgb(215, 201, 207);
                        }
                    }
                }
            }
        }

        #endregion

        #region Events
        private void MNGroupEnableDisable_Click(object sender, EventArgs e)
        {
            if (MNRemoveGroup.Text == "Disable Groups")
            {
                while (GridView1.GroupedColumns.Count != 0)
                {
                    GridView1.GroupedColumns[GridView1.GroupedColumns.Count - 1].UnGroup();
                }
                MNRemoveGroup.Text = "Enable Groups";
            }
            else
            {
                foreach (string Str in Val.ToString(Group_By_Tag).Split(','))
                {
                    if (Str != "")
                    {
                        GridView1.Columns[Str].Group();
                    }
                }
                MNRemoveGroup.Text = "Disable Groups";
            }
            ExpandTool_Click(null, null);
        }
        private void MNFilter_Click(object sender, EventArgs e)
        {
            GridView1.BeginUpdate();
            if (ISFilter == true)
            {
                ISFilter = false;
                MNFilter.Text = "Add Filter";
                GridView1.OptionsView.ShowAutoFilterRow = false;
            }
            else
            {
                ISFilter = true;
                MNFilter.Text = "Remove Filter";
                GridView1.OptionsView.ShowAutoFilterRow = true;
            }
            GridView1.EndUpdate();
        }
        private void EmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ObjPer.AllowEMail == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionEMailMsg);
                return;
            }

            string StrFile = Global.DataGridExportToExcel(GridView1, "Report");

            Utility.FrmEmailSend FrmEmailSend = new Utility.FrmEmailSend();
            FrmEmailSend.mStrSubject = lblReportHeader.Text;
            FrmEmailSend.mStrAttachments = StrFile;
            FrmEmailSend.ShowForm();
            FrmEmailSend = null;

            if (File.Exists(StrFile))
            {
                File.Delete(StrFile);
            }
            this.Focus();
        }
        private void TsmExportData_Click(object sender, EventArgs e)
        {
            if (Article_Name == "")
            {
                Global.Message("Enter Article Name For Export Data ...");
                return;
            }

            if (MSize_Name == "")
            {
                Global.Message("Enter MSize Name For Export Data ...");
                return;
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            ReportParams ObjReportParams = new ReportParams();
            this.Cursor = Cursors.WaitCursor;
            btnRefresh.Enabled = false;
            decimal numBalance = 0;

            if (FilterByFormName.Equals("FrmStockReport"))
            {
                if (Val.ToString(ObjReportDetailProperty.Remark) == "MIX_SPLIT_REPORT_DETAIL")
                {
                    decimal from_amount = 0;
                    decimal to_amount = 0;
                    decimal to_carat = 0;

                    int count = 0;

                    DTab = ObjReportParams.GetLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                    for (int i = 0; i < DTab.Rows.Count; i++)
                    {
                        DataTable details = DTab.Select("mixsplit_srno = " + DTab.Rows[i]["mixsplit_srno"]).CopyToDataTable();

                        count = count + 1;

                        //if (Val.ToString(DTab.Rows[i]["transaction_type"]) == "Rejection")
                        //{
                        //    from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                        //    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                        //}
                        //else if (Val.ToString(DTab.Rows[i]["transaction_type"]) == "")
                        //{
                        //    from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                        //    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                        //}
                        //else
                        //{
                        //    from_amount = Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                        //    to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                        //}

                        from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                        to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);
                        to_carat += Val.ToDecimal(DTab.Rows[i]["to_carat"]);


                        if (count == details.Rows.Count)
                        {
                            DTab.Rows[i]["diff_amount"] = Val.ToDecimal(to_amount - from_amount);
                            DTab.Rows[i]["diff_rate"] = Math.Round(Val.ToDecimal(to_amount - from_amount) / to_carat, 0);
                            DTab.Rows[i]["diff_per"] = Math.Round((Val.ToDecimal(to_amount - from_amount) / from_amount) * 100, 2);

                            count = 0;
                            from_amount = 0;
                            to_amount = 0;
                            to_carat = 0;
                        }
                    }
                }
                else
                {
                    DTab = new DataTable();
                    DTab = ObjReportParams.GetLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
                }
            }

            if (FilterByFormName.Equals("FrmMFGAsOnDateStockReport"))
            {
                DTab = new DataTable();

                DTab = ObjReportParams.GetMFGLiveStockASOnDate(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmGalaxyReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetGalaxyKapanReport(ReportParams_Property, "GALAXY_Kapan_Detail_Report");
            }
            if (FilterByFormName.Equals("FrmMFGStockReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetMFGLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmMFGRoughStockReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetMFGRoughStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmMFGJangedReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetMFGJangedData(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmMFGProcessReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetMFGProcessData(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmMFGStockReportNew"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetMFGLiveStockNew(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmProcessRecieveReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetIssuePendingStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmPriceList"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetPriceList(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmEmployeeSalaryReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetMFG4PReport(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmLotingReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetLotingData(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmStockIssuePendingReport"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetIssuePendingReport(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmEmployeeDetail"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.GetEmployeeDetail(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
            }
            if (FilterByFormName.Equals("FrmDailyLedgerBook"))
            {
                DTab = new DataTable();
                DTab = ObjReportParams.Get_Transaction_View_Report(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                if (Val.Trim(ReportParams_Property.ledger_id) != "")
                {
                    if (ObjReportDetailProperty.Remark == "Credit_Debit_Ledger_Book")
                    {
                        int count = DTab.Rows.Count;
                        foreach (DataRow Dr in DTab.Rows)
                        {
                            if (Val.ToDecimal(Dr["credit_amount"]) > 0 || Val.ToDecimal(Dr["debit_amount"]) > 0)
                            {
                            }
                            else
                            {
                                count--;
                                if (count > 0)
                                {
                                    Dr[0] = string.Empty;
                                    Dr.Delete();
                                }
                            }
                        }
                    }
                }
                if (Val.ToString(ObjReportDetailProperty.Remark) == "Payment_Pending_Remark")
                {
                    foreach (DataRow Drw in DTab.Rows)
                    {
                        numBalance = numBalance + Val.ToDecimal(Drw["opening_amount"]) + Val.ToDecimal(Drw["sale_amount"]) - Val.ToDecimal(Drw["receive_amount"]);
                        Drw["closing_amount"] = numBalance;
                    }
                }

                if (Val.ToString(ObjReportDetailProperty.Remark) == "Daily Ledger Book")
                {
                    foreach (DataRow Drw in DTab.Rows)
                    {
                        numBalance = numBalance + Val.ToDecimal(Drw["Opening_Amt"]) + Val.ToDecimal(Drw["Credit_Amt"]) - Val.ToDecimal(Drw["Debit_Amt"]);
                        Drw["Balance"] = numBalance;
                    }
                }

                if (Val.ToString(ObjReportDetailProperty.Remark) == "Daily Ledger Book Cash Balance")
                {
                    foreach (DataRow Drw in DTab.Rows)
                    {
                        numBalance = numBalance + Val.ToDecimal(Drw["Opening_Amt"]) + Val.ToDecimal(Drw["Credit_Amt"]) - Val.ToDecimal(Drw["Debit_Amt"]);
                        Drw["Balance"] = numBalance;
                    }
                }

                if (Val.ToString(ObjReportDetailProperty.Remark) == "Party_Ledger")
                {
                    string strLedgerNew = string.Empty;
                    string strLedgerOld = string.Empty;
                    foreach (DataRow Drw in DTab.Rows)
                    {
                        strLedgerNew = Val.ToString(Drw["ledger_name"]);

                        if (strLedgerOld == strLedgerNew)
                        {
                            numBalance = numBalance + Val.ToDecimal(Drw["debit_amount"]) - Val.ToDecimal(Drw["credit_amount"]);
                            Drw["balance_amount"] = numBalance;
                        }
                        else
                        {
                            numBalance = 0;
                            numBalance = Val.ToDecimal(Drw["debit_amount"]) - Val.ToDecimal(Drw["credit_amount"]);
                            Drw["balance_amount"] = numBalance;
                        }

                        strLedgerOld = Val.ToString(Drw["ledger_name"]);
                    }
                }

                if (Val.ToString(ObjReportDetailProperty.Remark) == "MIX_SPLIT_REPORT_DETAIL")
                {
                    decimal from_amount = 0;
                    decimal to_amount = 0;
                    int count = 0;

                    DTab = ObjReportParams.GetLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);

                    for (int i = 0; i < DTab.Rows.Count; i++)
                    {
                        DataTable details = DTab.Select("mixsplit_srno = " + DTab.Rows[i]["mixsplit_srno"]).CopyToDataTable();
                        count = count + 1;

                        from_amount += Val.ToDecimal(DTab.Rows[i]["from_amount"]);
                        to_amount += Val.ToDecimal(DTab.Rows[i]["to_amount"]);

                        if (count == details.Rows.Count)
                        {
                            DTab.Rows[i]["diff_amount"] = Val.ToDecimal(to_amount - from_amount);
                            count = 0;
                            from_amount = 0;
                            to_amount = 0;
                        }
                    }
                }
            }
            GridControl1.DataSource = DTab;
            GridControl1.RefreshDataSource();
            GridControl1.Refresh();
            GridView1.RefreshData();
            this.Cursor = Cursors.Default;
            btnRefresh.Enabled = true;
        }
        private static void SetLvlBackColor(DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventArgs e, Color lvlBackColor)
        {
            e.LevelAppearance.BackColor = lvlBackColor;
            e.LevelAppearance.ForeColor = Color.Black;
        }

        #endregion

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //if (ObjPer.AllowPrint == false)
            //{
            //    Global.Message(BLL.GlobalDec.gStrPermissionPrintMsg);
            //Enabled = false;
            DialogResult result = MessageBox.Show("Do you want to Barcode Print?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                BtnPrint.Enabled = true;
                return;
            }

            BarcodePrint printBarCode = new BarcodePrint();

            DataTable dtCheckedBarcode = (DataTable)GridControl1.DataSource;
            dtCheckedBarcode.AcceptChanges();
            //if (dtCheckedBarcode.Select("SEL = 'True' ").Length > 0)
            //{
            //    dtCheckedBarcode = dtCheckedBarcode.Select("SEL = 'True' ").CopyToDataTable();
            for (int i = 0; i < dtCheckedBarcode.Rows.Count; i++)
            {
                string Sub_lot_no = Val.ToString(dtCheckedBarcode.Rows[i]["print_kap_no"].ToString());
                
                if (Sub_lot_no.ToString() != "")
                {
                    if (dtCheckedBarcode.Rows[i]["print_stone_no"] != null && dtCheckedBarcode.Rows[i]["print_carat"].ToString() != "")
                    {
                        printBarCode.AddPktGalaxy(dtCheckedBarcode.Rows[i]["print_process_name"].ToString(), Sub_lot_no, dtCheckedBarcode.Rows[i]["print_stone_no"].ToString(),
                            Val.ToInt(dtCheckedBarcode.Rows[i]["print_tops"]), Val.ToDecimal(dtCheckedBarcode.Rows[i]["print_rough_weight"]), Val.ToDecimal(dtCheckedBarcode.Rows[i]["print_carat"]), Val.ToDecimal(dtCheckedBarcode.Rows[i]["print_p_carat"]), true);
                    }
                }

            }
            printBarCode.GalaxyPrintTSC();
            //}
            //else
            //{
            //    Global.Message("Please Select One Lot Atleast.");
            //    BtnPrint.Enabled = true;
            //    return;
            //}
            BtnPrint.Enabled = true;
        }
    }
}
