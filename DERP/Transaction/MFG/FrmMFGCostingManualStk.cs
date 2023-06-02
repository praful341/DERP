using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;
using Excel = Microsoft.Office.Interop.Excel;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGCostingManualStk : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        Excel.Application myExcelApplication;
        Excel.Workbook myExcelWorkbook;
        Excel.Worksheet myExcelWorkSheet;
        Validation Val = new Validation();
        DataTable dtPrint = new DataTable();

        public DataTable DTab = new DataTable();
        public string ColumnsToHide = "";
        public bool AllowMultiSelect = false;
        public string ColumnHeaderCaptions = "";
        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";

        FormEvents objBOFormEvents = new FormEvents();
        public FrmMFGCostingManual FrmMFGCostingManual = new FrmMFGCostingManual();
        string FormName = "";
        int m_numSelectedCount = 0;
        DataTable DtAssortment = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        DataTable m_dtbColor = new DataTable();
        FillCombo ObjFillCombo = new FillCombo();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtCut = new DataTable();
        DataTable m_dtbParam = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        #endregion

        #region Constructor
        public FrmMFGCostingManualStk()
        {
            InitializeComponent();
        }

        public void ShowForm(FrmMFGCostingManual ObjForm, Int32 Type)
        {
            FrmMFGCostingManual = new FrmMFGCostingManual();
            FrmMFGCostingManual = ObjForm;
            FormName = "FrmMFGCostingManual";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();

            this.Text = "Costing Manual Stock";

            if (Type == 1)
            {
                RBtnType.SelectedIndex = 0;
            }
            else
            {
                RBtnType.SelectedIndex = 1;
            }
            this.ShowDialog();
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Form Events
        private void FrmJangedConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DTab.Columns.Contains("SEL") == false)
                {

                    if (DTab.Columns.Contains("SEL") == false)
                    {

                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        DTab.Columns.Add(Col);
                    }
                }
                DTab.Columns["SEL"].SetOrdinal(0);

                MainGrid.DataSource = DTab;
                MainGrid.RefreshDataSource();

                foreach (DevExpress.XtraGrid.Columns.GridColumn Col in GrdDet.Columns)
                {
                    if (Col.FieldName.ToUpper() == "SEL")
                    {
                        Col.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        Col.OptionsColumn.AllowEdit = false;
                    }
                }

                GrdDet.BestFitColumns();

                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021
            }
            catch (Exception ex)
            {
                Global.ErrorMessage(ex.Message);
            }
        }
        private void FrmJangedConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void BtnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            MFGCostingManual objMFGCostingManual = new MFGCostingManual();
            MFGCostingManualProperty objMFGCostingManualProperty = new MFGCostingManualProperty();
            objMFGCostingManualProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
            objMFGCostingManualProperty.from_date = Val.DBDate(dtpFromDate.Text);
            objMFGCostingManualProperty.to_date = Val.DBDate(dtpToDate.Text);
            if (RBtnType.EditValue.ToString() == "1")
            {
                objMFGCostingManualProperty.location_id = Val.ToInt32(1);
            }
            else
            {
                objMFGCostingManualProperty.location_id = Val.ToInt32(2);
            }

            DTab = objMFGCostingManual.GetCostingManualStock(objMFGCostingManualProperty);

            if (DTab.Rows.Count > 0)
            {
                if (DTab.Columns.Contains("SEL") == false)
                {
                    if (DTab.Columns.Contains("SEL") == false)
                    {
                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        DTab.Columns.Add(Col);
                    }
                }
                DTab.Columns["SEL"].SetOrdinal(0);

                foreach (DevExpress.XtraGrid.Columns.GridColumn Col in GrdDet.Columns)
                {
                    if (Col.FieldName.ToUpper() == "SEL")
                    {
                        Col.OptionsColumn.AllowEdit = true;
                    }
                    else
                    {
                        Col.OptionsColumn.AllowEdit = false;
                    }
                }

                MainGrid.DataSource = (DataTable)DTab;
                MainGrid.RefreshDataSource();
                GrdDet.BestFitColumns();
            }
            else
            {
                Global.Message("Data Not Found...");
                MainGrid.DataSource = null;
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                //if (m_dtbParam.Rows.Count == 0)
                //{
                //    m_dtbParam = DTab_KapanWiseData;
                //}
            }
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Grid Events
        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SEL")
            {
                if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SEL")) == true)
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", false);

                }
                else
                {
                    GrdDet.SetRowCellValue(e.RowHandle, "SEL", true);
                }
            }
        }
        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "SEL")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSelectedCount;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        #endregion

        #region Other Function

        private void Export1(string format, string dlgHeader, string dlgFilter)
        {
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
                            GrdDet.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            GrdDet.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            GrdDet.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            GrdDet.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            GrdDet.ExportToText(Filepath);
                            break;
                        case "html":
                            GrdDet.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            GrdDet.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
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

                //Excel.Range line = (Excel.Range)myExcelWorkSheet.Rows[1];
                //line.Insert();
                //myExcelWorkSheet.Cells[1, 1] = "Report Name :- " + ReportHeaderName;
                //Excel.Range line1 = (Excel.Range)myExcelWorkSheet.Rows[2];
                //line1.Insert();
                ////myExcelWorkSheet.Cells[2, 1] = "Group :- " + lblGroupBy.Text;
                //Excel.Range line2 = (Excel.Range)myExcelWorkSheet.Rows[3];
                //line2.Insert();
                ////myExcelWorkSheet.Cells[3, 1] = "Parameters :- " + lblFilter.Text;
                //Excel.Range line1 = (Excel.Range)myExcelWorkSheet.Rows[2];
                //line1.Insert();
                //myExcelWorkSheet.Cells[2, 1] = "Print Date :- " + System.DateTime.Now.ToString();

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
        #endregion

        #region Repository Events

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string StrLotList = "";
            for (int i = 0; i < GrdDet.DataRowCount; i++)
            {
                if (GrdDet.GetRowCellValue(i, "SEL").ToString().ToUpper() == "TRUE")
                {
                    if (StrLotList.Length > 0)
                    {
                        StrLotList = StrLotList + "," + GrdDet.GetRowCellValue(i, "cost_id").ToString();
                    }
                    else
                    {
                        StrLotList = GrdDet.GetRowCellValue(i, "cost_id").ToString();
                    }
                }
            }
            if (FormName == "FrmMFGCostingManual")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("cost_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGCostingManual.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
        }

        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
            Export("xlsx", GrdDet);
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            Export1("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export1("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export1("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export1("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export1("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion  
    }
}