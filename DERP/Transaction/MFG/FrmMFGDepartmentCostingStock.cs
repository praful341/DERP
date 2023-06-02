using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGDepartmentCostingStock : DevExpress.XtraEditors.XtraForm
    {

        #region Declaration

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
        public FrmMFGDepartmentCosting FrmMFGDepartmentCosting = new FrmMFGDepartmentCosting();
        string FormName = "";
        int m_numSelectedCount = 0;
        DataTable DtAssortment = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        DataTable m_dtbColor = new DataTable();
        FillCombo ObjFillCombo = new FillCombo();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtCut = new DataTable();
        DataTable m_dtbParam = new DataTable();
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        #endregion

        #region Constructor
        public FrmMFGDepartmentCostingStock()
        {
            InitializeComponent();
        }

        public void ShowForm(FrmMFGDepartmentCosting ObjForm, DataTable DTab_List)
        {
            FrmMFGDepartmentCosting = new FrmMFGDepartmentCosting();
            FrmMFGDepartmentCosting = ObjForm;
            FormName = "FrmMFGDepartmentCosting";
            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.Text = "Department Costing";

            DTab = DTab_List.Copy();

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

        #region Events

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                MFGDepartmentCosting objMFGDepartmentCosting = new MFGDepartmentCosting();
                MFGDepartmentCostingProperty objMFGDepartmentCostingProperty = new MFGDepartmentCostingProperty();
                objMFGDepartmentCostingProperty.department_id = Val.ToInt64(lueDepartment.EditValue);
                objMFGDepartmentCostingProperty.year = Val.ToInt(txtYear.Text);
                objMFGDepartmentCostingProperty.month = Val.ToInt(txtMonth.Text);
                objMFGDepartmentCostingProperty.from_date = Val.DBDate(dtpFromDate.Text);
                objMFGDepartmentCostingProperty.to_date = Val.DBDate(dtpToDate.Text);
                this.Cursor = Cursors.WaitCursor;

                DTab = objMFGDepartmentCosting.GetDepartmentCostingData(objMFGDepartmentCostingProperty);

                this.Cursor = Cursors.Default;

                if (DTab.Rows.Count > 0)
                {
                    ChkAll.Visible = true;
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
                    MainGrid.DataSource = DTab;
                    MainGrid.RefreshDataSource();
                    GrdDet.BestFitColumns();
                }
                else
                {
                    Global.Message("Data Not Found...");
                    ChkAll.Checked = false;
                    MainGrid.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void repSelect_QueryValueByCheckState(object sender, DevExpress.XtraEditors.Controls.QueryValueByCheckStateEventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            object val = edit.EditValue;
            Int64 Lot_Srno;
            switch (e.CheckState)
            {
                case CheckState.Checked:
                    if (val is bool)
                        e.Value = true;
                    Lot_Srno = Val.ToInt64(GrdDet.GetRowCellValue(GrdDet.FocusedRowHandle, "lot_srno"));
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        if (Val.ToInt64(GrdDet.GetRowCellValue(IntI, "lot_srno")) == Lot_Srno)
                        {
                            GrdDet.SetRowCellValue(IntI, "SEL", e.Value);
                        }
                    }
                    break;
                case CheckState.Unchecked:
                    if (val is bool)
                        e.Value = false;
                    Lot_Srno = Val.ToInt64(GrdDet.GetRowCellValue(GrdDet.FocusedRowHandle, "lot_srno"));
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        if (Val.ToInt64(GrdDet.GetRowCellValue(IntI, "lot_srno")) == Lot_Srno)
                        {
                            GrdDet.SetRowCellValue(IntI, "SEL", e.Value);
                        }
                    }
                    break;
            }
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

        private void Export(string format, string dlgHeader, string dlgFilter)
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
        #endregion

        #region Repository Events

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string StrLotList = "";

            DataTable dtCheckedBarcode = (DataTable)MainGrid.DataSource;
            dtCheckedBarcode.AcceptChanges();
            if (dtCheckedBarcode.Select("SEL = 'True' ").Length > 1)
            {
                Global.Message("Please Select One Lot Atleast.");
                return;
            }

            for (int i = 0; i < GrdDet.DataRowCount; i++)
            {
                if (GrdDet.GetRowCellValue(i, "SEL").ToString().ToUpper() == "TRUE")
                {
                    if (StrLotList.Length > 0)
                    {
                        StrLotList = StrLotList + "," + GrdDet.GetRowCellValue(i, "lot_srno").ToString();
                    }
                    else
                    {
                        StrLotList = GrdDet.GetRowCellValue(i, "lot_srno").ToString();
                    }
                }
            }
            if (FormName == "FrmMFGDepartmentCosting")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_srno in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGDepartmentCosting.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
        }

        private void GrdDet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Q)
            {
                try
                {
                    if (ChkAll.Checked)
                    {
                        ChkAll.Checked = false;
                    }
                    else
                    {
                        ChkAll.Checked = true;
                    }
                    for (int i = 0; i < GrdDet.RowCount; i++)
                    {
                        if (ChkAll.Checked == true)
                        {
                            bool b = true;
                            GrdDet.SetRowCellValue(i, "SEL", b);
                        }
                        else
                        {
                            bool b = false;
                            GrdDet.SetRowCellValue(i, "SEL", b);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region Checkbox Events

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GrdDet.RowCount; i++)
                {
                    if (ChkAll.Checked == true)
                    {
                        bool b = true;
                        GrdDet.SetRowCellValue(i, "SEL", b);
                    }
                    else
                    {
                        bool b = false;
                        GrdDet.SetRowCellValue(i, "SEL", b);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }
        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }
        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }
        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion

        private void FrmMFGDepartmentCostingStock_Load(object sender, EventArgs e)
        {
            try
            {
                ChkAll.Visible = true;
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

                Global.LOOKUPDepartment_Costing(lueDepartment);

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
            }
            catch (Exception ex)
            {
                Global.ErrorMessage(ex.Message);
            }
        }

        private void FrmMFGDepartmentCostingStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}