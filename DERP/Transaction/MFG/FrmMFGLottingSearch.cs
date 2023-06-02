using BLL;
using BLL.FunctionClasses.Transaction;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGLottingSearch : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration


        Validation Val = new Validation();
        MFGProcessReceive objProcessRecieve;
        DataTable dtPrint = new DataTable();
        DataTable m_dtbKapan = new DataTable();
        DataTable m_dtbParam = new DataTable();
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
        public FrmMFGMixSplitLotting FrmMFGMixSplitLotting = new FrmMFGMixSplitLotting();
        public FrmMFGKapanMixing FrmMFGKapanMixing = new FrmMFGKapanMixing();
        public FrmMFGProcessIssueFactory FrmMFGProcessIssueFactory = new FrmMFGProcessIssueFactory();
        public FrmMFGMultiEployeeReturn FrmMFGMultiEployeeReturn = new FrmMFGMultiEployeeReturn();
        public FrmMFGGalaxyMultiEployeeReturn FrmMFGGalaxyMultiEployeeReturn = new FrmMFGGalaxyMultiEployeeReturn();
        public FrmMFGMixSplit FrmMFGMixSplit = new FrmMFGMixSplit();
        public FrmMFGMixSplitLottingSarin FrmMFGMixSplitLottingSarin = new FrmMFGMixSplitLottingSarin();
        public FrmMFGMixSplitAssortment FrmMFGMixSplitAssortment = new FrmMFGMixSplitAssortment();

        string FormName = "";
        int m_numSelectedCount = 0;
        string m_TransType = "";
        #endregion

        #region Constructor
        public FrmMFGLottingSearch()
        {
            InitializeComponent();
            objProcessRecieve = new MFGProcessReceive();
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
        public void ShowForm(FrmMFGMixSplitAssortment ObjForm)
        {
            FrmMFGMixSplitAssortment = new FrmMFGMixSplitAssortment();
            FrmMFGMixSplitAssortment = ObjForm;
            FormName = "FrmMFGMixSplitAssortment";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGMixSplitLottingSarin ObjForm)
        {
            FrmMFGMixSplitLottingSarin = new FrmMFGMixSplitLottingSarin();
            FrmMFGMixSplitLottingSarin = ObjForm;
            FormName = "FrmMFGMixSplitLottingSarin";

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
        public void ShowForm(FrmMFGKapanMixing ObjForm1)
        {
            FrmMFGKapanMixing = new FrmMFGKapanMixing();
            FrmMFGKapanMixing = ObjForm1;
            FormName = "FrmMFGKapanMixing";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGProcessIssueFactory ObjForm1)
        {
            FrmMFGProcessIssueFactory = new FrmMFGProcessIssueFactory();
            FrmMFGProcessIssueFactory = ObjForm1;
            FormName = "FrmMFGProcessIssueFactory";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGMultiEployeeReturn ObjForm1)
        {
            FrmMFGMultiEployeeReturn = new FrmMFGMultiEployeeReturn();
            FrmMFGMultiEployeeReturn = ObjForm1;
            FormName = "FrmMFGMultiEployeeReturn";

            Val.frmGenSetForPopup(this);
            AttachFormEvents();
            this.ShowDialog();
        }
        public void ShowForm(FrmMFGGalaxyMultiEployeeReturn ObjForm1)
        {
            FrmMFGGalaxyMultiEployeeReturn = new FrmMFGGalaxyMultiEployeeReturn();
            FrmMFGGalaxyMultiEployeeReturn = ObjForm1;
            FormName = "FrmMFGGalaxyMultiEployeeReturn";

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

                dtpFromDate.EditValue = DateTime.Now;
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dtab = new DataTable();
            if (FormName == "FrmMFGMixSplitLotting")
            {
                dtab = objProcessRecieve.GetSearchLotting(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
            }
            else if (FormName == "FrmMFGMixSplitAssortment")
            {
                dtab = objProcessRecieve.GetSearchLotting(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
            }
            else if (FormName == "FrmMFGMixSplit")
            {
                dtab = objProcessRecieve.GetSearchMixSplit(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
            }
            else if (FormName == "FrmMFGKapanMixing")
            {
                dtab = objProcessRecieve.GetSearchKapan(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));

            }
            else if (FormName == "FrmMFGProcessIssueFactory")
            {
                dtab = objProcessRecieve.GetSearchProcessIssue(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToString(FormName));

            }
            else if (FormName == "FrmMFGMultiEployeeReturn")
            {
                dtab = objProcessRecieve.GetSearchProcessIssue(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToString(FormName));

            }
            else if (FormName == "FrmMFGGalaxyMultiEployeeReturn")
            {
                dtab = objProcessRecieve.GetSearchProcessIssue(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue), Val.ToString(FormName));

            }
            else if (FormName == "FrmMFGMixSplitLottingSarin")
            {
                dtab = objProcessRecieve.GetSearchMixSplit(Val.ToString(dtpFromDate.Text), Val.ToString(dtpToDate.Text), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueProcess.EditValue), Val.ToInt(lueSubProcess.EditValue));
            }
            MainGrid.DataSource = (DataTable)dtab;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
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
                        if (FormName == "FrmMFGMixSplitLotting")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "mixsplit_union_id"));
                            m_TransType = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "transaction_type"));
                            FrmMFGMixSplitLotting.FillGrid(unionId, m_TransType);

                        }
                        else if (FormName == "FrmMFGMixSplitAssortment")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "mixsplit_union_id"));
                            m_TransType = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "transaction_type"));
                            FrmMFGMixSplitAssortment.FillGrid(unionId, m_TransType);

                        }
                        else if (FormName == "FrmMFGMixSplit")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "mixsplit_union_id"));
                            m_TransType = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "transaction_type"));
                            FrmMFGMixSplit.FillGrid(unionId, m_TransType);

                        }
                        else if (FormName == "FrmMFGKapanMixing")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "union_id"));
                            FrmMFGKapanMixing.FillGrid(unionId);
                        }
                        else if (FormName == "FrmMFGMixSplitLottingSarin")
                        {
                            unionId = Val.ToInt(GrdDet.GetRowCellValue(e.RowHandle, "mixsplit_union_id"));
                            m_TransType = Val.ToString(GrdDet.GetRowCellValue(e.RowHandle, "transaction_type"));
                            FrmMFGMixSplitLottingSarin.FillGrid(unionId, m_TransType);

                        }
                        this.Close();
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

            if (FormName == "FrmMFGMixSplitLotting")
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
            else if (FormName == "FrmMFGMixSplitAssortment")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGMixSplitAssortment.GetStockData(DTab_Select);
                }
                else
                {
                    Global.Message("Data Are Not Selected");
                    return;
                }
            }
            else if (FormName == "FrmMFGMixSplitLottingSarin")
            {
                if (StrLotList.Length > 0)
                {
                    DataTable DTab_Select = DTab.Select("lot_id in(" + StrLotList + ")").CopyToDataTable();
                    this.Close();
                    FrmMFGMixSplitLottingSarin.GetStockData(DTab_Select);
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
            //if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Q)
            //{
            //    try
            //    {
            //        if (ChkAll.Checked)
            //        {
            //            ChkAll.Checked = false;
            //        }
            //        else
            //        {
            //            ChkAll.Checked = true;
            //        }
            //        for (int i = 0; i < GrdDet.RowCount; i++)
            //        {
            //            if (ChkAll.Checked == true)
            //            {
            //                bool b = true;
            //                GrdDet.SetRowCellValue(i, "SEL", b);
            //            }
            //            else
            //            {
            //                bool b = false;
            //                GrdDet.SetRowCellValue(i, "SEL", b);
            //            }
            //        }
            //        GetSummary();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
        }

        #endregion

        #region Checkbox Events

        //private void ChkAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i < GrdDet.RowCount; i++)
        //        {
        //            if (ChkAll.Checked == true)
        //            {
        //                bool b = true;
        //                GrdDet.SetRowCellValue(i, "SEL", b);
        //            }
        //            else
        //            {
        //                bool b = false;
        //                GrdDet.SetRowCellValue(i, "SEL", b);
        //            }
        //        }
        //        GetSummary();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (Val.ToBoolean(GrdDet.GetRowCellValue(e.RowHandle, "SEL")) == false)
            //    {
            //        e.Appearance.BackColor = Color.White;
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = Color.SkyBlue;
            //    }
            //}
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