using BLL;
using BLL.FunctionClasses.Transaction;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGSearchProcess : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration


        Validation Val = new Validation();
        MFGProcessReceive objProcessRecieve;
        DataTable dtPrint = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtbParam = new DataTable();
        DataTable m_dtbSubProcess = new DataTable();
        public DataTable DTab = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();
        public string ColumnsToHide = "";
        public bool AllowMultiSelect = false;
        public string ColumnHeaderCaptions = "";
        public string SearchText = "";
        public string SearchField = "";
        public string ValueMember = "";
        public string SelectedValue = "";

        FormEvents objBOFormEvents = new FormEvents();
        public FrmMFGProcessIssue FrmMFGProcessIssue = new FrmMFGProcessIssue();
        public FrmMFGProcessWeightLossRecieve FrmMFGProcessRecieve = new FrmMFGProcessWeightLossRecieve();
        public FrmMFGDepartmentTransfer FrmMFGDepartmentTransfer = new FrmMFGDepartmentTransfer();
        public FrmMFGJangedIssue FrmMFGJangedIssue = new FrmMFGJangedIssue();
        public FrmMFGJangedIssueLotting FrmMFGJangedIssueLotting = new FrmMFGJangedIssueLotting();
        public FrmMFGMixSplit FrmMFGMixSplit = new FrmMFGMixSplit();
        public FrmMFGMixSplitLotting FrmMFGMixSplitLotting = new FrmMFGMixSplitLotting();
        public FrmMFGProcessWeightLossRecieve FrmMFGProcessWeightLossRecieve = new FrmMFGProcessWeightLossRecieve();
        public FrmMFGLSAssortFinal FrmMFGLSAssortFinal = new FrmMFGLSAssortFinal();
        public FrmMFGJangedReturn FrmMFGJangedReturn = new FrmMFGJangedReturn();
        public FrmMFGSawableRecieve FrmMFGSawableRecieve = new FrmMFGSawableRecieve();
        public FrmMFGProcessChipiyoRecieve FrmMFGProcessChipiyoRecieve = new FrmMFGProcessChipiyoRecieve();
        public FrmMFGProcessReceive FrmMFGProcessReceive = new FrmMFGProcessReceive();
        public FrmMFGProcessReceiveJanged FrmMFGProcessReceiveJanged = new FrmMFGProcessReceiveJanged();
        public FrmMFGProcessReceiveWithSplit FrmMFGProcessReceiveWithSplit = new FrmMFGProcessReceiveWithSplit();
        public FrmMFGLotSplit FrmMFGLotSplit = new FrmMFGLotSplit();
        //public FrmMFGLotSplit FrmMFGLotSplit = new FrmMFGLotSplit();
        string FormName = "";
        int m_numSelectedCount = 0;
        #endregion

        #region Constructor
        public FrmMFGSearchProcess()
        {
            InitializeComponent();
            objProcessRecieve = new MFGProcessReceive();
        }
        public void ShowForm(FrmMFGProcessIssue ObjForm)
        {
            FrmMFGProcessIssue = new FrmMFGProcessIssue();
            FrmMFGProcessIssue = ObjForm;
            FormName = "FrmMFGProcessIssue";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }

        public void ShowForm(FrmMFGDepartmentTransfer ObjForm)
        {
            FrmMFGDepartmentTransfer = new FrmMFGDepartmentTransfer();
            FrmMFGDepartmentTransfer = ObjForm;
            FormName = "FrmMFGDepartmentTransfer";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }

        public void ShowForm(FrmMFGJangedIssue ObjForm)
        {
            FrmMFGJangedIssue = new FrmMFGJangedIssue();
            FrmMFGJangedIssue = ObjForm;
            FormName = "FrmMFGJangedIssue";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGJangedIssueLotting ObjForm)
        {
            FrmMFGJangedIssueLotting = new FrmMFGJangedIssueLotting();
            FrmMFGJangedIssueLotting = ObjForm;
            FormName = "FrmMFGJangedIssueLotting";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }

        public void ShowForm(FrmMFGMixSplit ObjForm)
        {
            FrmMFGMixSplit = new FrmMFGMixSplit();
            FrmMFGMixSplit = ObjForm;
            FormName = "FrmMFGMixSplit";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }

        public void ShowForm(FrmMFGMixSplitLotting ObjForm)
        {
            FrmMFGMixSplitLotting = new FrmMFGMixSplitLotting();
            FrmMFGMixSplitLotting = ObjForm;
            FormName = "FrmMFGMixSplitLotting";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGJangedReturn ObjForm)
        {
            FrmMFGJangedReturn = new FrmMFGJangedReturn();
            FrmMFGJangedReturn = ObjForm;
            FormName = "FrmMFGJangedReturn";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGProcessWeightLossRecieve ObjForm)
        {
            FrmMFGProcessWeightLossRecieve = new FrmMFGProcessWeightLossRecieve();
            FrmMFGProcessWeightLossRecieve = ObjForm;
            FormName = "FrmMFGProcessWeightLossRecieve";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }

        public void ShowForm(FrmMFGLSAssortFinal ObjForm)
        {
            FrmMFGLSAssortFinal = new FrmMFGLSAssortFinal();
            FrmMFGLSAssortFinal = ObjForm;
            FormName = "FrmMFGLSAssortFinal";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGSawableRecieve ObjForm)
        {
            FrmMFGSawableRecieve = new FrmMFGSawableRecieve();
            FrmMFGSawableRecieve = ObjForm;
            FormName = "FrmMFGSawableRecieve";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGProcessChipiyoRecieve ObjForm)
        {
            FrmMFGProcessChipiyoRecieve = new FrmMFGProcessChipiyoRecieve();
            FrmMFGProcessChipiyoRecieve = ObjForm;
            FormName = "FrmMFGProcessChipiyoRecieve";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGProcessReceive ObjForm)
        {
            FrmMFGProcessReceive = new FrmMFGProcessReceive();
            FrmMFGProcessReceive = ObjForm;
            FormName = "FrmMFGProcessReceive";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGProcessReceiveJanged ObjForm)
        {
            FrmMFGProcessReceiveJanged = new FrmMFGProcessReceiveJanged();
            FrmMFGProcessReceiveJanged = ObjForm;
            FormName = "FrmMFGProcessReceiveJanged";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGProcessReceiveWithSplit ObjForm)
        {
            FrmMFGProcessReceiveWithSplit = new FrmMFGProcessReceiveWithSplit();
            FrmMFGProcessReceiveWithSplit = ObjForm;
            FormName = "FrmMFGProcessReceiveWithSplit";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGLotSplit ObjForm)
        {
            FrmMFGLotSplit = new FrmMFGLotSplit();
            FrmMFGLotSplit = ObjForm;
            FormName = "FrmMFGLotSplit";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
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
                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtbParam = Global.GetRoughCutAll();

                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

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
        #endregion

        #region Events
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
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtab = new DataTable();

            if (FormName != "")
            {
                dtab = objProcessRecieve.GetSearchProcess(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToString(FormName));
            }

            MainGrid.DataSource = (DataTable)dtab;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value)
            {
                if (m_dtbSubProcess.Rows.Count > 0)
                {
                    DataTable dtbdetail = m_dtbSubProcess;

                    string strFilter = string.Empty;

                    if (lueProcess.Text != "")
                        strFilter = "process_id = " + lueProcess.EditValue;

                    dtbdetail.DefaultView.RowFilter = strFilter;
                    dtbdetail.DefaultView.ToTable();

                    DataTable dtb = dtbdetail.DefaultView.ToTable();

                    lueSubProcess.Properties.DataSource = dtb;
                    lueSubProcess.Properties.ValueMember = "sub_process_id";
                    lueSubProcess.Properties.DisplayMember = "sub_process_name";
                    lueSubProcess.EditValue = System.DBNull.Value;
                }
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
                //CalculateTotal();
            }
        }
        private void GrdDet_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        int unionId = 0;
                        //DataRow Drow = GrdDet.GetDataRow(e.RowHandle);
                        unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "union_id"));

                        if (FormName == "FrmMFGProcessReceiveWithSplit")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "issue_id"));
                            this.Close();
                            FrmMFGProcessReceiveWithSplit.FillGrid(unionId);

                        }
                        else if (FormName == "FrmMFGLSAssortFinal")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "issue_id"));
                            this.Close();
                            FrmMFGLSAssortFinal.FillGrid(unionId);

                        }
                        else if (FormName == "FrmMFGJangedReturn")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "issue_id"));
                            this.Close();
                            FrmMFGJangedReturn.FillGrid(unionId);

                        }
                        else if (FormName == "FrmMFGProcessIssue")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "lot_srno"));
                            this.Close();
                            FrmMFGProcessIssue.FillGrid(unionId);
                        }
                        else
                        {
                            this.Close();
                            FrmMFGProcessIssue.FillGrid(unionId);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        #endregion

        #region Other Function
        private void GetSummary()
        {
            try
            {
                double IntSelPcs = 0; double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)MainGrid.DataSource;
                GrdDet.PostEditor();
                Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]);
                            }
                        }
                    }
                }
                //txtSelLot.Text = IntSelLot.ToString();
                //txtSelPcs.Text = IntSelPcs.ToString();
                //txtSelCarat.Text = DouSelCarat.ToString();
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
            for (int i = 0; i < GrdDet.DataRowCount; i++)
            {
                if (GrdDet.GetRowCellValue(i, "SEL").ToString().ToUpper() == "TRUE")
                {
                    if (StrLotList.Length > 0)
                    {
                        StrLotList = StrLotList + "," + GrdDet.GetRowCellValue(i, "lot_id").ToString();
                    }
                    else
                    {
                        StrLotList = GrdDet.GetRowCellValue(i, "lot_id").ToString();
                    }
                }
            }

            if (FormName == "FrmMFGProcessIssue")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGProcessIssue.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGDepartmentTransfer")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGDepartmentTransfer.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGMixSplit")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGMixSplit.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            if (FormName == "FrmMFGJangedIssue")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGJangedIssue.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            if (FormName == "FrmMFGJangedIssueLotting")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGJangedIssueLotting.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGMixSplitLotting")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGMixSplitLotting.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGProcessWeightLossRecieve")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGProcessWeightLossRecieve.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGLSAssortFinal")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGLSAssortFinal.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGJangedReturn")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGJangedReturn.GetStockData(DTab_Select);
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

        }

        #endregion

        #region Checkbox Events


        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

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
    }
}