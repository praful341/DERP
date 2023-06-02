using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using DERP.Report;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGPataLotEntryStock : DevExpress.XtraEditors.XtraForm
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
        public FrmMFGPatalotEntry FrmMFGPatalotEntry = new FrmMFGPatalotEntry();
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
        public FrmMFGPataLotEntryStock()
        {
            InitializeComponent();
        }

        public void ShowForm(FrmMFGPatalotEntry ObjForm, DataTable DTab_List)
        {
            FrmMFGPatalotEntry = new FrmMFGPatalotEntry();
            FrmMFGPatalotEntry = ObjForm;
            FormName = "FrmMFGPatalotEntry";
            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.Text = "PataLot Entry";

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

        #region Form Events
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
                MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
                MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();
                objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                objMFGPataLotEntryProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                objMFGPataLotEntryProperty.from_date = Val.DBDate(dtpFromDate.Text);
                objMFGPataLotEntryProperty.to_date = Val.DBDate(dtpToDate.Text);
                this.Cursor = Cursors.WaitCursor;

                DTab = objMFGPataLotEntry.GetPendingStock(objMFGPataLotEntryProperty);

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
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            m_dtbParam = new DataTable();
            if (lueKapan.Text.ToString() != "")
            {
                m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
            }
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
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
            GetSummary();
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
        private void repSelect_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }

        #endregion

        #region Other Function
        private void GetSummary()
        {
            try
            {
                double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)MainGrid.DataSource;
                GrdDet.PostEditor();
                //Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                //IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["total_carat"]);
                            }
                        }
                    }
                }
                txtSelLot.Text = IntSelLot.ToString();
                //txtSelPcs.Text = IntSelPcs.ToString();
                txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
            }
        }
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

        private void repSelect_MouseUp(object sender, MouseEventArgs e)
        {
            GetSummary();
        }

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
            if (FormName == "FrmMFGPatalotEntry")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_srno in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGPatalotEntry.GetStockData(DTab_Select);
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
                    GetSummary();
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
                GetSummary();
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

        private void FrmMFGPataLotEntryStock_Load(object sender, EventArgs e)
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

                GridGroupSummaryItem item4 = new GridGroupSummaryItem();
                item4.FieldName = "lot_id";
                item4.SummaryType = DevExpress.Data.SummaryItemType.Count;
                item4.ShowInGroupColumnFooter = GrdDet.Columns["lot_id"];
                GrdDet.GroupSummary.Add(item4);

                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = "pcs";
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.ShowInGroupColumnFooter = GrdDet.Columns["pcs"];
                GrdDet.GroupSummary.Add(item1);

                GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                item2.FieldName = "carat";
                item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item2.ShowInGroupColumnFooter = GrdDet.Columns["carat"];
                GrdDet.GroupSummary.Add(item2);

                GrdDet.BestFitColumns();

                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

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

                GetSummary();
                txtSelLot.EditValue = "";
                txtSelCarat.EditValue = "";
            }
            catch (Exception ex)
            {
                Global.ErrorMessage(ex.Message);
            }
        }

        private void FrmMFGPataLotEntryStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to PataLot Entry Print?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
            {
                BtnPrint.Enabled = true;
                return;
            }

            string StrLotList = "";
            DataTable dtCheckedBarcode = (DataTable)MainGrid.DataSource;
            dtCheckedBarcode.AcceptChanges();
            if (dtCheckedBarcode.Select("SEL = 'True' ").Length > 0)
            {
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
                if (FormName == "FrmMFGPatalotEntry")
                {
                    if (StrLotList.Length > 0)
                    {
                        DataTable DTab_Select = DTab.Select("lot_srno in(" + StrLotList + ")").CopyToDataTable();

                        // FrmMFGPatalotEntry.GetStockData(DTab_Select);

                        DataTable DtPataLotPrintData = new DataTable();
                        MFGPataLotEntry objMFGPataLotEntry = new MFGPataLotEntry();
                        MFGPataLotEntryProperty objMFGPataLotEntryProperty = new MFGPataLotEntryProperty();

                        //objMFGPataLotEntryProperty.rough_cut_id = Val.ToInt64(lueRoughCutNo.EditValue);
                        //objMFGPataLotEntryProperty.kapan_id = Val.ToInt64(lueKapan.EditValue);
                        objMFGPataLotEntryProperty.lot_srno_list = Val.ToString(StrLotList);

                        DtPataLotPrintData = objMFGPataLotEntry.MFGPataLotEntryPrintGetData(objMFGPataLotEntryProperty);

                        decimal p_numLoning_Per = 0;
                        decimal p_numMathala_Weight = 0;
                        decimal p_numKachha_Weight = 0;

                        foreach (DataRow Drw in DtPataLotPrintData.Rows)
                        {
                            Drw["k_per"] = 0;
                            if (Val.ToDecimal(Drw["k_carat"]) > 0)
                            {
                                Drw["koning_per"] = Math.Round((Val.ToDecimal(Drw["koning_carat"]) / Val.ToDecimal(Drw["k_carat"])) * 100, 2);
                            }
                            if (Val.ToDecimal(Drw["koning_carat"]) > 0)
                            {
                                Drw["tal_per"] = Math.Round((Val.ToDecimal(Drw["tal_carat"]) / Val.ToDecimal(Drw["koning_carat"])) * 100, 2);
                            }
                            if (Val.ToDecimal(Drw["tal_carat"]) > 0)
                            {
                                Drw["pel_per"] = Math.Round((Val.ToDecimal(Drw["pel_carat"]) / Val.ToDecimal(Drw["tal_carat"])) * 100, 2);
                            }
                            if (Val.ToDecimal(Drw["pel_carat"]) > 0)
                            {
                                Drw["matala_per"] = Math.Round((Val.ToDecimal(Drw["matala_carat"]) / Val.ToDecimal(Drw["pel_carat"])) * 100, 2);
                            }
                            p_numLoning_Per = Val.ToDecimal(Drw["koning_carat"]);
                            p_numMathala_Weight = Val.ToDecimal(Drw["matala_carat"]);
                            p_numKachha_Weight = Val.ToDecimal(Drw["k_carat"]);

                            if (p_numLoning_Per > 0)
                            {
                                Drw["pata_ok_per"] = Val.ToString(Math.Round((Val.ToDecimal(Drw["t_wt"]) / p_numLoning_Per) * 100, 2));
                            }
                            if (p_numKachha_Weight > 0)
                            {
                                Drw["total_per"] = Val.ToString(Math.Round((Val.ToDecimal(p_numMathala_Weight) / p_numKachha_Weight) * 100, 2));
                            }
                        }

                        FrmReportViewer FrmReportViewer = new FrmReportViewer();
                        FrmReportViewer.DS.Tables.Add(DtPataLotPrintData);
                        FrmReportViewer.GroupBy = "";
                        FrmReportViewer.RepName = "";
                        FrmReportViewer.RepPara = "";
                        this.Cursor = Cursors.Default;
                        FrmReportViewer.AllowSetFormula = true;

                        FrmReportViewer.ShowForm("PataLot_Entry_Part2", 120, FrmReportViewer.ReportFolder.PATA_LOT_ENTRY);

                        DtPataLotPrintData = null;
                        FrmReportViewer.DS.Tables.Clear();
                        FrmReportViewer.DS.Clear();
                        FrmReportViewer = null;
                    }
                    else
                    {
                        Global.Message("Data Are Not Selected");
                        return;
                    }
                }
            }
            else
            {
                Global.Message("Please Select One Lot Atleast.");
                return;
            }
        }
    }
}