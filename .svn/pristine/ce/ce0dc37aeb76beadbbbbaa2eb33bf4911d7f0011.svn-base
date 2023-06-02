using BLL;
using BLL.FunctionClasses.Master.Store;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction.Store;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction.Store;
using DevExpress.Data;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid;
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
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Global = DERP.Class.Global;
namespace DERP.Report
{
    public partial class FrmStoreReportViewerBand : DevExpress.XtraEditors.XtraForm
    {
        #region Delcaration

        Excel.Application myExcelApplication;
        Excel.Workbook myExcelWorkbook;
        Excel.Worksheet myExcelWorkSheet;

        private DataSet _DS = new DataSet();
        public DataSet DS
        {
            get { return _DS; }
            set { _DS = value; }
        }

        public New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();
        NewReportMaster ObjReport = new NewReportMaster();
        New_Report_MasterProperty ObjReportMasterProperty = new New_Report_MasterProperty();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();

        ReportParams ObjReportParams = new ReportParams();
        FillCombo ObjFillCombo = new FillCombo();
        MfgItemTypeMaster objItemType = new MfgItemTypeMaster();
        MfgPartyGroupMaster objPartyGroup = new MfgPartyGroupMaster();
        StoreManagerMaster objStoreManager = new StoreManagerMaster();
        MfgItemMaster objStoreItem = new MfgItemMaster();
        StorePartyMaster objStoreParty = new StorePartyMaster();
        DataTable DTab_Item_Condition = new DataTable();
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        BLL.FormPer ObjPer = new BLL.FormPer();

        string MergeOnStr = string.Empty;
        string MergeOn = string.Empty;
        Boolean ISFilter = false;
        public Boolean IsPivot = false;

        public string RepName = string.Empty;
        public string RepPara = string.Empty;
        public string GroupBy = string.Empty;
        public bool AllowSetFormula = false;

        public int ReceiptDays = 0;
        public double DouExpDiff = 0;
        int mIntReportCode = 0;
        public string BandConsumeCaption = "";
        public string BandConsumeWithProcessCaption = "";

        /// <summary>
        ////
        /// </summary>

        decimal DouQty = 0;
        decimal DouAmount = 0;

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

        private DataSet _DTab_Set = new DataSet();

        public DataSet DTab_Set
        {
            get { return _DTab_Set; }
            set { _DTab_Set = value; }
        }

        private string _Group_By_Tag;

        public string Group_By_Tag
        {
            get { return _Group_By_Tag; }
            set { _Group_By_Tag = value; }
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

        public string R_Date;
        public string Shape_Name;
        public string Article_Name;
        public string MSize_Name;

        private string _Procedure_Name;

        public string Procedure_Name
        {
            get { return _Procedure_Name; }
            set { _Procedure_Name = value; }
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
        public FrmStoreReportViewerBand()
        {
            InitializeComponent();
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
                myExcelWorkSheet.Cells[1, 1] = "Report Name :- " + ReportHeaderName;
                //Excel.Range line1 = (Excel.Range)myExcelWorkSheet.Rows[2];
                //line1.Insert();
                ////myExcelWorkSheet.Cells[2, 1] = "Group :- " + lblGroupBy.Text;
                //Excel.Range line2 = (Excel.Range)myExcelWorkSheet.Rows[3];
                //line2.Insert();
                ////myExcelWorkSheet.Cells[3, 1] = "Parameters :- " + lblFilter.Text;
                Excel.Range line1 = (Excel.Range)myExcelWorkSheet.Rows[2];
                line1.Insert();
                myExcelWorkSheet.Cells[2, 1] = "Print Date :- " + System.DateTime.Now.ToString();

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
            mIntReportCode = Val.ToInt(this.Tag);

            ObjFillCombo.user_id = GlobalDec.gEmployeeProperty.user_id;

            //RBtnDepartmentIssueType.Visible = true;
            //RBtnDepartmentReturnType.Visible = false;
            //RBtnSalesType.Visible = false;
            //RBtnStockType.Visible = false;
            //RBtnPurchaseType.Visible = false;
            //RBtnDepartmentIssueType.SelectedIndex = 0;

            ListReportType_EditValueChanged(null, null);

            DataTable ItemType = objItemType.GetData(1);
            ItemType.DefaultView.Sort = "item_type_name";
            ItemType = ItemType.DefaultView.ToTable();

            ListItemType.Properties.DataSource = ItemType;
            ListItemType.Properties.DisplayMember = "item_type_name";
            ListItemType.Properties.ValueMember = "item_type_id";

            DataTable PartyGroup = objPartyGroup.GetData(1);
            PartyGroup.DefaultView.Sort = "party_group_name";
            PartyGroup = PartyGroup.DefaultView.ToTable();

            ListPaymentType.Properties.DataSource = PartyGroup;
            ListPaymentType.Properties.DisplayMember = "party_group_name";
            ListPaymentType.Properties.ValueMember = "party_group_id";

            DTab_Item_Condition.Columns.Add("item_condition");
            DTab_Item_Condition.Rows.Add("working");
            DTab_Item_Condition.Rows.Add("repairing");
            DTab_Item_Condition.Rows.Add("reject");

            ListItemCondition.Properties.DataSource = DTab_Item_Condition;
            ListItemCondition.Properties.ValueMember = "item_condition";
            ListItemCondition.Properties.DisplayMember = "item_condition";

            //DataTable StoreManager = objStoreManager.GetData();
            //StoreManager.DefaultView.Sort = "manager_name";
            //StoreManager = StoreManager.DefaultView.ToTable();

            //ListManager.Properties.DataSource = StoreManager;
            //ListManager.Properties.DisplayMember = "manager_name";
            //ListManager.Properties.ValueMember = "manager_id";

            DataTable DTabLocation = ObjFillCombo.FillCmb(FillCombo.TABLE.Location_Master);
            DTabLocation.DefaultView.Sort = "Location_Name";
            DTabLocation = DTabLocation.DefaultView.ToTable();

            ListLocation.Properties.DataSource = DTabLocation;
            ListLocation.Properties.DisplayMember = "Location_Name";
            ListLocation.Properties.ValueMember = "location_id";

            //DataTable DTabDivision = ObjFillCombo.FillCmb(FillCombo.TABLE.Division_Master);
            //DTabDivision.DefaultView.Sort = "division_name";
            //DTabDivision = DTabDivision.DefaultView.ToTable();

            //ListDivision.Properties.DataSource = DTabDivision;
            //ListDivision.Properties.DisplayMember = "division_name";
            //ListDivision.Properties.ValueMember = "division_id";

            StoreDepartmentMaster objStoreDepartment = new StoreDepartmentMaster();

            DataTable DTabDepartment = objStoreDepartment.GetData();
            DTabDepartment.DefaultView.Sort = "department_name";
            DTabDepartment = DTabDepartment.DefaultView.ToTable();

            ListDepartment.Properties.DataSource = DTabDepartment;
            ListDepartment.Properties.DisplayMember = "department_name";
            ListDepartment.Properties.ValueMember = "department_id";

            DataTable DTabBranch = ObjFillCombo.FillCmb(FillCombo.TABLE.Branch_Master);
            DTabBranch.DefaultView.Sort = "Branch_Name";
            DTabBranch = DTabBranch.DefaultView.ToTable();

            ListBranch.Properties.DataSource = DTabBranch;
            ListBranch.Properties.DisplayMember = "Branch_Name";
            ListBranch.Properties.ValueMember = "branch_id";

            //DataTable DTabStoreItem = objStoreItem.GetData();
            //DTabStoreItem.DefaultView.Sort = "item_name";
            //DTabStoreItem = DTabStoreItem.DefaultView.ToTable();

            //ListItem.Properties.DataSource = DTabStoreItem;
            //ListItem.Properties.DisplayMember = "item_name";
            //ListItem.Properties.ValueMember = "item_id";

            DataTable DTabStoreParty = objStoreParty.GetData();
            DTabStoreParty.DefaultView.Sort = "party_name";
            DTabStoreParty = DTabStoreParty.DefaultView.ToTable();

            ListParty.Properties.DataSource = DTabStoreParty;
            ListParty.Properties.DisplayMember = "party_name";
            ListParty.Properties.ValueMember = "party_id";

            FillControls();

            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            //DTPFromDate.EditValue = DateTime.Now;
            DTPFromDate.EditValue = "01/Nov/2021";

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;
            DTPFromDate.Focus();
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
            GridControl1.DataSource = null;
            GridView1.Columns.Clear();
            GridView1.Bands.Clear();
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

                GridView1.Bands.AddBand(ReportHeaderName);

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
                                //StrFormat = "###################0.000";
                                StrFormat = "###################,##,###,0.000";
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
                    if (Val.ToString(Report_Type).ToUpper().Contains("SUMMARY"))
                    {
                        IntCount = StrGroupBy.Length - 1;
                    }
                    else
                    {
                        IntCount = StrGroupBy.Length;
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

                //if (RBtnType.Text == "PARTY WISE BALANCE SHEET DETAIL")
                //{
                //    GridView1.Columns["from_party"].Fixed = FixedStyle.Left;
                //    GridView1.ClearGrouping();
                //    GridView1.Columns["from_party"].GroupIndex = 0;
                //    GridView1.OptionsView.ShowGroupedColumns = false;
                //    GridView1.ExpandAllGroups();
                //}
            }
            catch (Exception Ex)
            {
                Global.Message("Error In Column Index : " + IntError.ToString() + "    " + Ex.Message);
            }
        }
        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title
            TextBrick BrickTitle = e.Graph.DrawString(ReportHeaderName, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Tahoma", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //// ' For Group 
            //TextBrick BrickTitleseller = e.Graph.DrawString("Group :- " + lblGroupBy.Text, System.Drawing.Color.Navy, new RectangleF(0, 25, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitleseller.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitleseller.ForeColor = Color.Black;

            //// ' For Filter 
            //TextBrick BrickTitlesParam = e.Graph.DrawString("Parameters :- " + lblFilter.Text, System.Drawing.Color.Navy, new RectangleF(0, 40, e.Graph.ClientPageSize.Width, 60), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitlesParam.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitlesParam.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString(), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
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
        }
        private void GridView1_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;

            #region  Sales

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "SALES DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouQty = 0;
                    DouAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouQty = DouQty + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "qty"));
                    DouAmount = DouAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouQty > 0)
                        {
                            e.TotalValue = Math.Round((DouAmount / DouQty), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Purchase

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "PURCHASE DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouQty = 0;
                    DouAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouQty = DouQty + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "qty"));
                    DouAmount = DouAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouQty > 0)
                        {
                            e.TotalValue = Math.Round((DouAmount / DouQty), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Department Issue

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEPARTMENT ISSUE DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouQty = 0;
                    DouAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouQty = DouQty + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "qty"));
                    DouAmount = DouAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouQty > 0)
                        {
                            e.TotalValue = Math.Round((DouAmount / DouQty), 2);
                        }
                        else
                        {
                            e.TotalValue = 0;
                        }
                    }
                }
            }
            #endregion

            #region  Department Return

            if (Val.ToString(ObjReportDetailProperty.Remark).ToUpper() == "DEPARTMENT RETURN DETAIL")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouQty = 0;
                    DouAmount = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouQty = DouQty + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "qty"));
                    DouAmount = DouAmount + Val.ToDecimal(GridView1.GetRowCellValue(e.RowHandle, "amount"));
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (((GridSummaryItem)e.Item).FieldName.CompareTo("rate") == 0)
                    {
                        if (DouQty > 0)
                        {
                            e.TotalValue = Math.Round((DouAmount / DouQty), 2);
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
            FrmEmailSend.mStrSubject = ReportHeaderName;
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
        private void RbtReportType_EditValueChanged(object sender, EventArgs e)
        {
            ObjReportDetailProperty = ObjReport.GetReportDetailProperty(mIntReportCode, Val.ToString(RbtReportType.EditValue));
            mDTDetail = ObjReport.GetDataForSearchSettings(mIntReportCode, Val.ToString(RbtReportType.EditValue));
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            GridControl1.DataSource = null;
            GridView1.Columns.Clear();
            GridView1.Bands.Clear();

            for (int i = 0; i < ListItemType.Properties.Items.Count; i++)
                ListItemType.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListItemCondition.Properties.Items.Count; i++)
                ListItemCondition.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListManager.Properties.Items.Count; i++)
                ListManager.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListDepartment.Properties.Items.Count; i++)
                ListDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListLocation.Properties.Items.Count; i++)
                ListLocation.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListItem.Properties.Items.Count; i++)
                ListItem.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListParty.Properties.Items.Count; i++)
                ListParty.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListPaymentType.Properties.Items.Count; i++)
                ListPaymentType.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListBranch.Properties.Items.Count; i++)
                ListBranch.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListDivision.Properties.Items.Count; i++)
                ListDivision.Properties.Items[i].CheckState = CheckState.Unchecked;
            DTPFromDate.Focus();
        }
        private void BtnExport_Click(object sender, EventArgs e)
        {
            Export("xlsx", GridView1);
        }
        private void BtnPrint_Click(object sender, EventArgs e)
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

        private static void SetLvlBackColor(DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventArgs e, Color lvlBackColor)
        {
            e.LevelAppearance.BackColor = lvlBackColor;
            e.LevelAppearance.ForeColor = Color.Black;
        }
        private void backgroundWorker_StoreReport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ReportParams_Property.entry_type = Val.ToString(CmbReport.Text);
            DTab = ObjReportParams.GetStoreReport(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
        }

        private void backgroundWorker_StoreReport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            FrmStoreReportViewerBand FrmGReportViewer = new Report.FrmStoreReportViewerBand();
            PanelLoading.Visible = false;

            FrmGReportViewer.ObjReportDetailProperty = ObjReportDetailProperty;
            FrmGReportViewer.mDTDetail = mDTDetail;
            //FrmGReportViewer.Report_Type = Val.ToString(RbtReportType.EditValue);
            FrmGReportViewer.Report_Code = ObjReportDetailProperty.Report_code;

            FrmGReportViewer.Remark = ObjReportDetailProperty.Remark;
            FrmGReportViewer.ReportParams_Property = ReportParams_Property;
            FrmGReportViewer.Procedure_Name = ObjReportDetailProperty.Procedure_Name;
            FrmGReportViewer.FilterByFormName = this.Name;

            ObjReportDetailProperty.Remark = "";
            FrmGReportViewer.Remark = "";

            ObjReportDetailProperty.Remark = "";
            FrmGReportViewer.Remark = "";

            if (ListReportType.Text == "SALES")
            {
                if (CmbReport.Text == "SALES DETAIL")
                {
                    ObjReportDetailProperty.Remark = "SALES DETAIL";
                    FrmGReportViewer.Remark = "SALES DETAIL";
                    FrmGReportViewer.ReportHeaderName = "Sales Detail";
                }
            }
            else if (ListReportType.Text == "PURCHASE")
            {
                if (CmbReport.Text == "PURCHASE DETAIL")
                {
                    ObjReportDetailProperty.Remark = "PURCHASE DETAIL";
                    FrmGReportViewer.Remark = "PURCHASE DETAIL";
                    FrmGReportViewer.ReportHeaderName = "Purchase Detail";
                }
            }
            else if (ListReportType.Text == "DEPARTMENT ISSUE")
            {
                if (CmbReport.Text == "DEPARTMENT ISSUE DETAIL")
                {
                    ObjReportDetailProperty.Remark = "DEPARTMENT ISSUE DETAIL";
                    FrmGReportViewer.Remark = "DEPARTMENT ISSUE DETAIL";
                    FrmGReportViewer.ReportHeaderName = "Department Issue Detail";
                }
            }
            else if (ListReportType.Text == "DEPARTMENT RETURN")
            {
                if (CmbReport.Text == "DEPARTMENT RETURN DETAIL")
                {
                    ObjReportDetailProperty.Remark = "DEPARTMENT RETURN DETAIL";
                    FrmGReportViewer.Remark = "DEPARTMENT RETURN DETAIL";
                    FrmGReportViewer.ReportHeaderName = "Department Return Detail";
                }
            }
            ReportHeaderName = FrmGReportViewer.ReportHeaderName;
            //FrmGReportViewer.FilterBy = GetFilterByValue();
            FrmGReportViewer.DTab = DTab;
            if (FrmGReportViewer.DTab == null || FrmGReportViewer.DTab.Rows.Count == 0)
            {
                this.Cursor = Cursors.Default;
                FrmGReportViewer.Dispose();
                FrmGReportViewer = null;
                Global.Message("Data Not Found");
                GridControl1.DataSource = null;
                GridView1.Columns.Clear();
                GridView1.Bands.Clear();
                return;
            }
            this.Cursor = Cursors.Default;
            ObjPer.Report_Code = Report_Code;
            int IntIndex = 0;
            int IntSelectedIndex = 0;
            foreach (System.Drawing.Printing.PaperKind foo in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
            {
                CmbPageKind.Items.Add(foo.ToString());

                IntIndex++;
            }
            CmbPageKind.SelectedIndex = IntSelectedIndex;

            FillGrid();
            FooterSummary();

            if (ListReportType.Text == "PURCHASE")
            {
                if (CmbReport.Text == "PURCHASE DETAIL")
                {
                    GridView1.ClearGrouping();
                    GridView1.Columns["p_bill_no"].GroupIndex = 0;
                    GridView1.OptionsView.ShowGroupedColumns = false;
                }
            }
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
            if (result > 0)
            {
                Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                return;
            }

            PanelLoading.Visible = true;
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);

            ReportParams_Property.item_condition = Val.Trim(ListItemCondition.Properties.GetCheckedItems());
            ReportParams_Property.item_type = Val.Trim(ListItemType.Properties.GetCheckedItems());
            ReportParams_Property.store_party = Val.Trim(ListParty.Properties.GetCheckedItems());
            ReportParams_Property.store_manager = Val.Trim(ListManager.Properties.GetCheckedItems());
            ReportParams_Property.store_item = Val.Trim(ListItem.Properties.GetCheckedItems());
            ReportParams_Property.payment_type = Val.Trim(ListPaymentType.Properties.GetCheckedItems());
            ReportParams_Property.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());
            ReportParams_Property.location_id = Val.Trim(ListLocation.Properties.GetCheckedItems());
            ReportParams_Property.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
            ReportParams_Property.division_id = Val.Trim(ListDivision.Properties.GetCheckedItems());

            if (this.backgroundWorker_StoreReport.IsBusy)
            {
            }
            else
            {
                backgroundWorker_StoreReport.RunWorkerAsync();
            }
        }
        private void FillControls()
        {
            DataTable DTab = ObjReport.GetDataForSearchDetailReport(mIntReportCode);

            foreach (DataRow DRow in DTab.Rows)
            {
                DevExpress.XtraEditors.Controls.RadioGroupItem rd = new DevExpress.XtraEditors.Controls.RadioGroupItem();
                rd.Tag = Val.ToString(DRow["report_type"]);
                rd.Value = Val.ToString(DRow["report_type"]);
                rd.Description = Val.ToString(DRow["report_type"]);
                RbtReportType.Properties.Items.Add(rd);

            }
            RbtReportType.SelectedIndex = 0;
            ObjReportMasterProperty = ObjReport.GetReportMasterProperty(mIntReportCode);
        }
        private void ListPartyGroup_EditValueChanged(object sender, EventArgs e)
        {
            //item objAvakJavakPartyMasterProperty = new AvakJavakParty_MasterProperty();

            ////objAvakJavakPartyMasterProperty.party_group_name = Val.Trim(ListPartyGroup.Properties.GetCheckedItems());

            //DataTable DTab = objAvakJavakParty.GetData_AvakJavakPartyGroup_Party(objAvakJavakPartyMasterProperty);
            //ListParty.Properties.DataSource = DTab;
            //ListParty.Properties.DisplayMember = "party_name";
            //ListParty.Properties.ValueMember = "party_id";
        }
        private void ListReportType_EditValueChanged(object sender, EventArgs e)
        {
            if (ListReportType.Text == "DEPARTMENT ISSUE")
            {
                CmbReport.Properties.Items.Clear();
                CmbReport.Properties.Items.Add("DEPARTMENT ISSUE DETAIL");
                CmbReport.Properties.Items.Add("DEPARTMENT ISSUE SUMMARY");
                //CmbReport.Properties.Items.Add("DEPARTMENT ISSUE DATEWISE");
                //CmbReport.Properties.Items.Add("DEPARTMENT ISSUE ITEM WISE");
                CmbReport.SelectedIndex = 0;
            }
            else if (ListReportType.Text == "DEPARTMENT RETURN")
            {
                CmbReport.Properties.Items.Clear();
                CmbReport.Properties.Items.Add("DEPARTMENT RETURN DETAIL");
                CmbReport.Properties.Items.Add("DEPARTMENT RETURN SUMMARY");
                //CmbReport.Properties.Items.Add("DEPARTMENT RETURN DATEWISE");
                //CmbReport.Properties.Items.Add("DEPARTMENT RETURN ITEM WISE");
                CmbReport.SelectedIndex = 0;
            }
            else if (ListReportType.Text == "PURCHASE")
            {
                CmbReport.Properties.Items.Clear();
                CmbReport.Properties.Items.Add("PURCHASE DETAIL");
                //CmbReport.Properties.Items.Add("ITEMWISE PURCHASE DETAIL");
                CmbReport.Properties.Items.Add("PURCHASE SUMMARY");
                //CmbReport.Properties.Items.Add("PARTY WISE PURCHASE");
                CmbReport.SelectedIndex = 0;
            }
            else if (ListReportType.Text == "SALES")
            {
                CmbReport.Properties.Items.Clear();
                CmbReport.Properties.Items.Add("SALES DETAIL");
                CmbReport.Properties.Items.Add("SALES SUMMARY");
                //CmbReport.Properties.Items.Add("ITEMWISE SALES DETAIL");
                //CmbReport.Properties.Items.Add("PARTY WISE SALES");
                //CmbReport.Properties.Items.Add("PARTY WISE SUMMARY");
                //CmbReport.Properties.Items.Add("ITEMWISE SALE SUMMARY");
                CmbReport.SelectedIndex = 0;
            }
            else if (ListReportType.Text == "STOCK")
            {
                CmbReport.Properties.Items.Clear();
                CmbReport.Properties.Items.Add("ITEM STOCK");
                CmbReport.Properties.Items.Add("ITEM STOCK DEPARTMENT WISE");
                CmbReport.Properties.Items.Add("PARTY STOCK");
                CmbReport.Properties.Items.Add("OUT OF STOCK");
                //CmbReport.Properties.Items.Add("STOCK ITEMWISE DETAIL");
                //CmbReport.Properties.Items.Add("DEPARTMENTWISE STOCK DETAIL");
                //CmbReport.Properties.Items.Add("STOCK DETAIL");
                //CmbReport.Properties.Items.Add("OUT ITEM STOCK");
                CmbReport.SelectedIndex = 0;
            }
        }

        private void ListItemType_EditValueChanged(object sender, EventArgs e)
        {
            if (ListItemType.EditValue.ToString() != "")
            {
                objStoreItem = new MfgItemMaster();
                DataTable DTabStoreItem = objStoreItem.Item_TypeWise_GetData(Val.Trim(ListItemType.Properties.GetCheckedItems()));
                DTabStoreItem.DefaultView.Sort = "item_name";
                DTabStoreItem = DTabStoreItem.DefaultView.ToTable();

                ListItem.Properties.DataSource = DTabStoreItem;
                ListItem.Properties.DisplayMember = "item_name";
                ListItem.Properties.ValueMember = "item_id";
            }
        }

        private void ListBranch_EditValueChanged(object sender, EventArgs e)
        {
            if (ListBranch.EditValue.ToString() != "" && ListDepartment.EditValue.ToString() != "")
            {
                Store_DepartmentIssueProperty objDeptIssueProperty = new Store_DepartmentIssueProperty();
                StoreDepartmentIssue objDeptIssue = new StoreDepartmentIssue();

                objDeptIssueProperty.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
                objDeptIssueProperty.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());

                DataTable DTab_Division = objDeptIssue.Division_List_GetData(objDeptIssueProperty);

                ListDivision.Properties.DataSource = DTab_Division;
                ListDivision.Properties.ValueMember = "division_id";
                ListDivision.Properties.DisplayMember = "division_name";
                ListDivision.ClosePopup();
            }
            else
            {
                ListDivision.Properties.Items.Clear();
            }
        }

        private void ListDepartment_EditValueChanged(object sender, EventArgs e)
        {
            if (ListBranch.EditValue.ToString() != "" && ListDepartment.EditValue.ToString() != "")
            {
                Store_DepartmentIssueProperty objDeptIssueProperty = new Store_DepartmentIssueProperty();
                StoreDepartmentIssue objDeptIssue = new StoreDepartmentIssue();

                objDeptIssueProperty.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
                objDeptIssueProperty.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());

                DataTable DTab_Division = objDeptIssue.Division_List_GetData(objDeptIssueProperty);

                ListDivision.Properties.DataSource = DTab_Division;
                ListDivision.Properties.ValueMember = "division_id";
                ListDivision.Properties.DisplayMember = "division_name";
                ListDivision.ClosePopup();
            }
            else
            {
                ListDivision.Properties.Items.Clear();
            }
        }
    }
}
